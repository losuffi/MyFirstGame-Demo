using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Self_class : NetworkBehaviour {
    //-----------------------------公共成員
    public string s_class;
    [SyncVar]public int s_id;
    public string s_name;
    public string s_Icontent;
    [SyncVar]public float p_life;
    [SyncVar]public float s_life;
    public bool isLife;
    //------------------------------!

    //-----------------------------委托回调
    public delegate void IsAttacked(GameObject Murderr);
    public event IsAttacked Attacked;
    public delegate void IsDead();
    public event IsDead EvisDead;
    //------------------------------!
    //-----------------------------Hero類成員 Equi类属性
    public string s_Controler;
    public float s_Scout=0;
    public int TalentFreePoint;
    public float s_speed;
    public float s_AttackValue;
    public float s_Defence;
    private float p_Hunger;
    public float s_HungerSpeed;
    public float s_Hunger;
    public Transform s_Canvas;
    public float s_GatherValue;
    //-------------------------------!

    //-------------------------------Item類
    public string s_iType;
    [SyncVar]public int s_iCount=1;
    [SyncVar]public bool isPick = true;
    public float s_speedInit;
    private float TimeH;
    public bool isTwiceItem;
    public bool isCast = false;
    public bool isCasting = false;
    //----------------------------------!
    //------------------------------建筑类
    [SyncVar] public GameObject Owner;
    //---------------------------------!
    private bool isDead = false;


    //------------------------------------函數
    public void injured(float damage,Transform Murderer)
    {
        if (isLife) 
        {
            if (isServer)
            {
                GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().RpcInjuredDis(Murderer.gameObject, this.gameObject);
            }
            if (Murderer.GetComponent<Self_class>().s_Canvas != null)
            {
                Murderer.GetComponent<Self_class>().s_Canvas.transform.FindChild("RLife").gameObject.SetActive(true);
                Murderer.GetComponent<Self_class>().s_Canvas.transform.FindChild("RLife").GetComponent<Rlife>().setTarget(this.gameObject);
            }
            damage = Mathf.Pow(1.008f, -s_Defence) * damage;
            if (s_class == "Hero" || s_class == "Monster")
            {
                GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
                if (p_life - damage <= 0)
                {
                    locplayer.GetComponent<UnitSyncCmd>().CmdLifeZero(this.gameObject);
                }
                else
                {
                    locplayer.GetComponent<UnitSyncCmd>().CmdChangeLife(-damage, this.gameObject);
                }
                if (Attacked != null)
                    Attacked(Murderer.gameObject);
            }
        }
    }
    public void CmdGather(float value,Transform GatherMan)
    {
        if (GatherMan.GetComponent<Self_class>().s_Canvas != null)
        {
            GatherMan.GetComponent<Self_class>().s_Canvas.transform.FindChild("RLife").gameObject.SetActive(true);
            GatherMan.GetComponent<Self_class>().s_Canvas.transform.FindChild("RLife").GetComponent<Rlife>().setTarget(this.gameObject);
        }
        if (!isLife) return;
        if (s_class == "Tree")
        {
            GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
            if (p_life - value > 0)
            {
                locplayer.GetComponent<UnitSyncCmd>().CmdChangeLife(-value, this.gameObject);
            }
            else
            {
                locplayer.GetComponent<UnitSyncCmd>().CmdLifeZero(this.gameObject);
                isLife = false;
            }
        }
    }
    public void Heal(float heal_v)
    {
        GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        if (isLife)
        {
            if (p_life+heal_v>s_life)
            {
                locplayer.GetComponent<UnitSyncCmd>().CmdLifeInit(this.gameObject);
            }
            else
            {
                locplayer.GetComponent<UnitSyncCmd>().CmdChangeLife(heal_v, this.gameObject);
            }
        }
        try
        {
            this.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis(this.transform.name + "被治愈，治愈量为：" + heal_v + ",当前生命值为：" + p_life);
        }
        catch { }
    }
    public float getLife()
    {
        return p_life;
    }
    public float getHunger()
    {
        return p_Hunger;
    }
    public void CmdDead()
    {
        isLife = false;
        if (EvisDead != null)
            EvisDead();
        if (s_class == "Tree")
        {
            EnvironmentCreate._ins.EnvBeGather(gameObject);
            GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdDes(this.gameObject);
        }
        else if (s_class == "Monster")
        {
            if (isServer)
            {
                transform.GetComponent<MonEnvBe>().BeItem();
                this.transform.GetComponent<Animator>().SetBool("isLife", false);
            }
        }
        else if(s_class=="Hero"&& s_Controler == "AI")
        {
            this.transform.GetComponent<Monster>().Dead();
            this.transform.GetComponent<Animator>().SetBool("isLife", false);
        }
        else if(s_class == "Hero" && s_Controler == "User")
        {
            transform.GetComponent<player>().Dead();
            s_Canvas.transform.FindChild("Gameover").gameObject.SetActive(true);
            s_Canvas.transform.FindChild("Gameover").GetComponent<GameOver>().GetScout();
        }
    }
    public void ChanHunger(float n)
    {
        if (p_Hunger + n > s_Hunger)
        {
            p_Hunger = s_Hunger;
        }
        else if (p_Hunger + n < 0)
        {
            p_Hunger = 0;
            injured(50, this.transform);
        }
        else
        {
            p_Hunger += n;
        }
    }
    void Start()
    {
        isLife = true;
        p_life = s_life;
        p_Hunger = s_Hunger;
        TimeH = Time.time;
        TalentFreePoint = 1;
        s_Defence = 0;
        initPos = this.transform.position;
    }
    private Vector3 initPos;
    void FixedUpdate()
    {
        isLife = (p_life > 0 ? true : false);
        if (!isDead && !isLife)
        {
            CmdDead();
            isDead = true;
        }
        if (Time.time - TimeH > 30)
        {
            TimeH = Time.time;
            ChanHunger(-s_HungerSpeed);
        }
        if (isLife&&this.transform.GetComponent<CharacterController>()!=null)
        {
            Clif();
        }
    }
    void Clif()
    {
       if (!this.transform.GetComponent<CharacterController>().isGrounded)
          {
            if (transform.position.y < -100)
            {
                if (s_class == "Hero" && s_Controler == "User")
                {
                    Debug.Log(this.transform.position);
                    CmdDead();
                    this.transform.position = initPos;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
          }
    }
    public void BindCanvas(Transform pUI)
    {
        s_Canvas = pUI;
    }
    //-----------------------------------hook
}