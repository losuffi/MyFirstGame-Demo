using UnityEngine;
using System.Collections;

public class FsM : MonoBehaviour
{
    private Transform Mo;
    private Transform Inductor;
    private float HoriSpeed=0;
    private float VertSpeed=0;
    private Vector3 dir;
    public enum FsmStatus
    {
        None,
        Move,
    }
    public FsmStatus mStatus;
    public enum Mission
    {
        Patrol,
        Chase,
    }
    public Mission mMission;
    public enum MissionStatus
    {
        isStart,
        isDone,
        isFail
    }
    //DefaultMission set:
    private Mission defaultMission = Mission.Patrol;
    //Patrol:
    private bool isPatrolChangeDirection = true;
    private MissionStatus sPatrol;
    private float PatrolInductorDistance = 20f;
    private Transform PatrolTarget;      //巡逻获取的目标
    private float PatrolTimeBuffer = 0;
    private float InductorAngles = 180;
    //Chase:
    private MissionStatus sChase;
    void Start()
    {
        dir = Vector3.zero;
        Mo = this.transform;
        Inductor = Mo.FindChild("Inductor");
        mMission = defaultMission;
        MissionInit(mMission);
    }
    public void EventBegin()
    {
        switch (mStatus)
        {
            case FsmStatus.None:
                break;
            case FsmStatus.Move:
                break;
            default:
                break;
        }
    }
    //*********************************************Monster动画
    private void Animator()
    {
        if (HoriSpeed > 0)
        {
            Mo.GetComponent<Animator>().SetInteger("MoStatus", 1);
        }
        else
        {
            Mo.GetComponent<Animator>().SetInteger("MoStatus", 0);
        }
    }
    //*****************************************************Mission 调度
    public void MissionInit(Mission n)
    {
        switch (n)
        {
            case Mission.Patrol:
                sPatrol = MissionStatus.isStart;
                break;
            case Mission.Chase:
                sChase = MissionStatus.isStart;
                break;
            default:
                break;
        }
    }
    public void MissionDone(Mission n)
    {
        switch (n)
        {
            case Mission.Patrol:
                sPatrol = MissionStatus.isDone;
                mMission = Mission.Chase;
                break;
            case Mission.Chase:
                sChase = MissionStatus.isDone;
                mMission = Mission.Patrol;
                break;
            default:
                break;
        }
        MissionInit(mMission);
    }
    public void MissionFail(Mission n)
    {
        switch (n)
        {
            case Mission.Patrol:
                sPatrol = MissionStatus.isFail;
                break;
            default:
                break;
        }
        MissionInit(mMission);
    }
    //*****************************************************Mission 实现
    //1、Patrol
    public void Patrol()
    {
        if (isPatrolChangeDirection)
        {
            int rnd = (int)Random.Range(0, 360);
            Mo.Rotate(Mo.up * rnd);
            isPatrolChangeDirection = false;
            HoriSpeed = Mo.GetComponent<Self_class>().s_speed;
        }
        RaycastHit hits;
        if (Physics.Raycast(this.transform.position, this.transform.forward,out hits, 16))
        {
            Debug.Log(hits.transform.name);
            if (hits.transform.tag == "Border")
            {
                isPatrolChangeDirection = true;
                Debug.Log("Border");
            }
        }
        if (IsHaveEnemy())
        {
            MissionDone(mMission);
        }
    }
    private bool IsHaveEnemy()
    {
        Vector3 eua = Inductor.eulerAngles;
        Inductor.Rotate(Inductor.up * InductorAngles/2);
        for (int i = 0; i < (InductorAngles / 30f); i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(Inductor.position, Inductor.forward, out hit, PatrolInductorDistance))
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
            Inductor.Rotate(Inductor.up * -30f);
        }
        Inductor.eulerAngles = eua;
        return false;
    }
    //2、Chase
    public void Chase()
    {
        Mo.LookAt(PatrolTarget);
        if (IsCanAttack())
        {
            MissionDone(mMission);
        }
    }
    private bool IsCanAttack()
    {
        return false;
    }
    //轮询
    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Animator();
        if (!controller.isGrounded)
        {
            VertSpeed -= 98*Time.deltaTime;
        }
        else
        {
            VertSpeed = 0;
        }
        if (sPatrol == MissionStatus.isStart)
        {
            if (Time.time - PatrolTimeBuffer > 10)
            {
                PatrolTimeBuffer = Time.time;
                isPatrolChangeDirection = true;
            }
            Patrol();
        }
        if (sChase == MissionStatus.isStart)
        {
            Chase();
        }
        //移动
        dir = (Mo.forward * HoriSpeed) + (Mo.up * VertSpeed);
        controller.Move(dir * Time.deltaTime);
    }
}
