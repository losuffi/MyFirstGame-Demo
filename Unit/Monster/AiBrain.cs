using UnityEngine;
using System.Collections;

public class AiBrain : BrainSpace
{
    public Transform AI;
    public Transform Brain;
    public Transform Inductor;
    public Transform Inductor_2;
    public Transform Inductor_3;
    public Transform Mo;
    public int[] SpellId;
    void Awake()
    {
        Acts = new Transform[6];
        Mo = this.transform.parent.parent;
        Brain = this.transform;
        Inductor = this.transform.parent.FindChild("Inductor");
        Inductor_2 = this.transform.parent.FindChild("Inductor_2");
        Inductor_3 = this.transform.parent.FindChild("Inductor_3");
        AI = this.transform.parent.parent;
    }
    //************************************************中心，基本思考调用模块
    void Start()
    {
        GuardInitPos = this.transform.position;
        ActReg();
        for(int i = 0; i < SpellId.Length; i++)
        {
            if (SpellId[i] > 100)
            {
                StudySpell(SpellId[i]);
            }
        }
        if (Character == Nature.Guard)
        {
            NowMission = Mission.Patrol_Guard;
        }
        else
        {
            NowMission = Mission.Patrol;
        }
        NowMissionStatus = Status.isStart;
        StartTimel = Time.time;
    }           
    void MissionCenter()
    {
        if (NowMissionStatus == Status.isGoing)
        {
            MissionGoing(NowMission);
        }
        else if(NowMissionStatus== Status.isStart)
        {
            StartMission(NowMission);
        }
        else if(NowMissionStatus == Status.isDone)
        {
            MissionDone(NowMission);
        }
    }
    void StartMission(Mission n)
    {
        for(byte j = 0; j < Acts.Length; j++)
        {
            ActBreak(j);
        }
        if (n == Mission.Patrol||n==Mission.Patrol_Guard)
        {
            StartTimel = Time.time;
            isIdel = false;
            ActWork(0);
            NowMissionStatus = Status.isGoing;
        }
        else if (n == Mission.Chase)
        {
            DecideTime = Time.time-3;
            NowMissionStatus = Status.isGoing;
        }
        else if(n == Mission.Escape)
        {
            NowMissionStatus = Status.isGoing;
        }
    }
    void MissionGoing(Mission n)
    {
        if (n == Mission.Patrol)
        {
            Patrol();
        }
        else if (n == Mission.Patrol_Guard)
        {
            Patrol_Guard();
        }
        else if (n == Mission.Chase)
        {
            Chase();
        }
        else if(n == Mission.Escape)
        {
            Escape();
        }
    }
    void MissionDone(Mission n)
    {
        if (n == Mission.Patrol||n==Mission.Patrol_Guard)
        {
            if (Character != Nature.Meek)
            {
                NowMission = Mission.Chase;
                NowMissionStatus = Status.isStart;
            }
            else
            {
                NowMission = Mission.Patrol;
                NowMissionStatus = Status.isStart;
            }
        }
        if (n == Mission.Chase)
        {
            NowMission = Mission.Patrol;
            NowMissionStatus = Status.isStart;
        }
    }
    //------------------------------------------------------------------!

    
    //*********************************************动作模块
    void ActWork(byte i) {
        if (Acts[i].GetComponent<ACT>().Status != "isGoing")
        {
            Acts[i].GetComponent<ACT>().Status = "isStart";
        }
    }
    void ActBreak(byte i)
    {
        Acts[i].GetComponent<ACT>().Status = "isDone";
    }
    void ActCenter()
    {
        for (byte i = 0; i < Acts.Length; i++)
        {
            if (Acts[i].GetComponent<ACT>() != null)
            {
                if (Acts[i].GetComponent<ACT>().Status =="isStart")
                {
                    ActStart(i);
                }
                else if(Acts[i].GetComponent<ACT>().Status == "isGoing")
                {
                    ActGoing(i);
                }
            }
        }
    }
    void ActStart(byte id)
    {
        if (id == 0)
        {
            ActTurnBuffer = 0;
            ActTurnLevel = Random.Range(0, 200);
            ActBreak(2);
        }
        if (id == 1)
        {
            ActBreak(2);
        }
        if (id == 2)
        {
            ActBreak(0);
            ActBreak(1);
        }
        if (id == 3)
        {
            ActBreak(0);
            ActTurnBuffer = 0;
            ActTurnLevel = 120;
            ActBreak(1);
        }
        if (id == 4)
        {
            ActBreak(2);
        }
        if (id == 5)
        {
            ActBreak(2);
        }
        Acts[id].GetComponent<ACT>().Status = "isGoing";
    }
    void ActGoing(byte id)
    {
        if (id == 0)
        {
            TurnDirection(ActTurnLevel);
        }
        if (id == 1)
        {
            Move();
        }
        if (id == 2)
        {
            Idel();
        }
        if (id == 3)
        {
            TurnDirection(ActTurnLevel);
        }
        if (id == 4)
        {
            Attack();
        }
        if (id == 5)
        {
            Spell(DecideResult);
        }
    }
    //------------------------------------------------------------------!


    //************************************************************任务具体的实现

    void Patrol()
    {
        if (isIdel)
        {
            ActWork(2);
            if (Time.time - StartTimel > 3)
            {
                StartTimel = Time.time;
                ActWork(0);
                isIdel = false;
            }
        }
        else
        {
            if (Time.time - StartTimel > 10)
            {
                isIdel = true;
                StartTimel = Time.time;
            }
            else
            {
                if (IsCliff())
                {
                    NowMissionStatus = Status.isStart;
                }
                else
                {
                    ActWork(1);
                }
            }
        }
        if (IsHaveEnemy())
        {
            NowMissionStatus = Status.isDone;
        }
    }
    private bool IsHaveEnemy()
    {
        Vector3 eua = Inductor.eulerAngles;
        Inductor.Rotate(Inductor.up * InductorAngles / 2);
        for (int i = 0; i < (InductorAngles / 5f); i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(Inductor.position, Inductor.forward, out hit, PatrolInductorDistance))
            {
                if (hit.transform.GetComponent<Self_class>() != null && hit.transform != Mo)
                {
                    if (hit.transform.GetComponent<Self_class>().s_class == "Hero"&& hit.transform.GetComponent<Self_class>().isLife&& hit.transform.GetComponent<Self_class>().s_Controler == "User")
                    {
                        PatrolTarget = hit.transform;
                        Inductor.eulerAngles = eua;
                        return true;
                    }
                }
            }
            Inductor.Rotate(Inductor.up * -5f);
        }
        Inductor.eulerAngles = eua;
        return false;
    }     //Patrol
    private bool IsCliff()
    {
        Vector3 eua = Inductor_2.eulerAngles;
        Inductor.Rotate(Inductor_2.up * (40 / 2));
        for (int i = 0; i < (40/2); i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(Inductor_2.position, Inductor_2.forward, out hit, PatrolInductorDistance))
            {
                if (hit.transform.tag == "Border")
                {
                    if (hit.distance < 4)
                    {
                        Inductor_2.eulerAngles = eua;
                        return true;
                    }
                }
            }
            Inductor_2.Rotate(Inductor.up * -2f);
        }
        Inductor_2.eulerAngles = eua;
        return false;
    }
    //--------------------------------------------------------! 
    void Patrol_Guard()
    {
        if (isIdel)
        {
            ActWork(2);
            if (Time.time - StartTimel > 3)
            {
                StartTimel = Time.time;
                ActWork(0);
                isIdel = false;
            }
        }
        else
        {
            if (Time.time - StartTimel > 10)
            {
                isIdel = true;
                StartTimel = Time.time;
            }
            else
            {
                if (IsCliff())
                {
                    NowMissionStatus = Status.isStart;
                }
                else
                {
                    GuardPos = GuardInitPos + Random.Range(0.5f, 1.1f) * GuardRadius * Mo.forward;
                    GuardPos = new Vector3(GuardPos.x, Mo.position.y, GuardPos.z);
                    Mo.LookAt(GuardPos);
                    ActWork(1);
                }
            }
        }
        if (IsHaveEnemy())
        {
            NowMissionStatus = Status.isDone;
        }
    }
    //--------------------------------------------------------! 
    void Chase()
    {
        if (Time.time - DecideTime > 3)
        {
            DecideTime = Time.time;
            byte a = (byte)Random.Range(mItemAddrMin, mHaveItemAddr);
            a++;
            DecideResult = (int)mSpell[a];
            if (IsCanDamageBySpell(DecideResult))
            {
                ActWork(5);
            }
        }
        else if (IsCanDamageBySpell(101))
        {
            if (!PatrolTarget.GetComponent<Self_class>().isLife)
            {
                NowMissionStatus = Status.isDone;
            }
            else
            {
                ActWork(4);
            }
        }
        if (!IsHaveObstacle())
        {
            targetPos = new Vector3(PatrolTarget.position.x, Mo.position.y, PatrolTarget.position.z);
            Mo.LookAt(targetPos);
            ActWork(1);
        }
        else
        {
            ActWork(3);
        }
        if(Vector3.Distance(Mo.position, targetPos) > 40)
        {
            ActWork(3);
            NowMissionStatus = Status.isDone;  
        }
    }
    private bool IsHaveObstacle()
    {
        Vector3 eua = Inductor_2.eulerAngles;
        Inductor.Rotate(Inductor_2.up * (40 / 2));
        for (int i = 0; i < (40 / 2); i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(Inductor_2.position, Inductor_2.forward, out hit, PatrolInductorDistance))
            {
                if (hit.transform.tag == "Environment")
                {
                    if (hit.distance < 2)
                    {
                        Inductor_2.eulerAngles = eua;
                        return true;
                    }
                }
            }
            Inductor_2.Rotate(Inductor.up * -2f);
        }
        Inductor_2.eulerAngles = eua;
        return false;
    }           //Chase
    private bool IsCanDamageBySpell(int sid)
    {
        float Distance;
        if (sid == 101)
        {
            Distance = 2;
        }
        else if(sid>101&&sid<=105)
        {
            Distance = 200;
        }
        else
        {
            return false;
        }
        Vector3 eua = Inductor.eulerAngles;
        Inductor.Rotate(Inductor.up * InductorAngles / 2);
        for (int i = 0; i < (InductorAngles / 5f); i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(Inductor.position,Inductor.forward,out hit,Distance))
            {
                if (hit.transform.GetComponent<Self_class>() != null && hit.transform != Mo)
                {
                    if (hit.transform.GetComponent<Self_class>().s_class == "Hero")
                    {
                        PatrolTarget = hit.transform;
                        Inductor.eulerAngles = eua;
                        return true;
                    }
                }
            }
            Inductor.Rotate(Inductor.up * -5f);
        }
        Inductor.eulerAngles = eua;
        return false;
    }
    //--------------------------------------------------------------!
    public void isAttacked(Transform Target)
    {
        if (Target.GetComponent<Self_class>().s_Controler != Mo.GetComponent<Self_class>().s_Controler)
        {
            PatrolTarget = Target;
            if (Character == Nature.Meek)
            {
                if (NowMission != Mission.Escape)
                {
                    NowMission = Mission.Escape;
                    NowMissionStatus = Status.isStart;
                }
            }
            else if (Character == Nature.Aggressive || Character == Nature.Guard)
            {
                if (NowMission != Mission.Chase)
                {
                    NowMission = Mission.Chase;
                    NowMissionStatus = Status.isStart;
                }
            }
        }
    }
    void Escape()
    {
        if (IsCliff())
        {
            ActWork(0);
        }
        else
        {
            if (!IsHaveObstacle())
            {
                targetPos = new Vector3(2*Mo.position.x-PatrolTarget.position.x, Mo.position.y, 2 * Mo.position.z-PatrolTarget.position.z);
                Mo.LookAt(targetPos);
                ActWork(1);
            }
            else
            {
                ActWork(3);
            }
        }
        if (Vector3.Distance(Mo.position, targetPos) > 40)
        {
            ActWork(3);
            NowMissionStatus = Status.isDone;
        }
    }
    //--------------------------------------------------------------!


    //*************************************************************动作的具体实现
    void TurnDirection(int level)
    {
        Mo.GetComponent<Monster>().TurnDirection();
        ActTurnBuffer++;
        if (ActTurnBuffer >= level)
        {
            Acts[0].GetComponent<ACT>().Status = "isDone";
        }
    }
    void Move()
    {
        Mo.GetComponent<Monster>().Move();
    }
    void Idel()
    {
        Mo.GetComponent<Monster>().Idel();
    }
    void Attack()
    {
        Mo.GetComponent<Monster>().Attack();
        Acts[4].GetComponent<ACT>().Status = "isDone";
    }
    void Spell(int sid)
    {
        Mo.GetComponent<Monster>().Spell(sid);
        Acts[5].GetComponent<ACT>().Status = "isDone";
    }
    //--------------------------------------------------------------------!




        //             ↓  激励---轮询
    void Update()
    {
        MissionCenter();
        ActCenter();
    }

}
