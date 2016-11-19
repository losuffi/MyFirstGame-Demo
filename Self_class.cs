using UnityEngine;
using System.Collections;
public class Self_class : MonoBehaviour {
    public string s_class;
    public string s_Controler;
    public float s_Scout=0;
    public int TalentFreePoint;
    public int s_id;
    public float s_life;
    public float s_speed;
    public string s_name;
    public string s_Icontent;
    public float s_AttackValue;
    public float s_Defence;
    private float p_life;
    private float p_Hunger;
    public float s_HungerSpeed;
    public float s_Hunger;
    public bool isLife;
    public string s_iType;
    public int s_iCount=1;
    public Canvas s_Canvas;
    public float s_speedInit;
    public float s_GatherValue;
    private float TimeH;
    public bool isTwiceItem;
    public bool isCast = false;
    public bool isCasting = false;
    public void injured(float damage,Transform Murderer)
    {
        if (isLife) 
        {
            damage = Mathf.Pow(1.008f, -s_Defence) * damage;
            if (s_class == "Hero" || s_class == "Monster")
            {
                if (p_life - damage > 0)
                {
                    p_life -= damage;
                }
                else
                {
                    p_life = 0;
                    if (Murderer.GetComponent<Self_class>().s_class == "Hero" && Murderer.GetComponent<Self_class>().s_Controler != s_Controler) 
                    {
                        Murderer.GetComponent<Self_class>().s_Scout += s_Scout;
                        try
                        {
                            Murderer.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis("你擊殺了" + s_name + ",獲取分數：" + s_Scout);
                        }
                        catch { }
                    }
                    Dead();
                }
                if (this.transform.GetComponent<Monster>() != null)
                {
                    this.transform.GetComponent<Monster>().isAttacked(Murderer);
                }
                try
                {
                    Murderer.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis(s_name + "被伤害，伤害量为：" + (int)damage + ",当前生命值为：" + p_life);
                }
                catch { }

                try
                {
                    this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis(s_name + "被伤害，伤害量为：" + (int)damage + ",当前生命值为：" + p_life);
                }
                catch { }
            }
        }
    }
    public void Gather(float value,Transform GatherMan)
    {
        if (s_class == "Tree")
        {
            if (p_life - value > 0)
            {
                p_life -= value;
            }
            else
            {
                p_life = 0;
                isLife = false;
                Dead();
            }
            try
            {
                GatherMan.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis(s_name + "正在被采集，采集程度：" + value);
            }
            catch { }
        }
    }
    public void Heal(float heal_v)
    {
        if (isLife)
        {
            if (p_life+heal_v>s_life)
            {
                p_life = s_life;
            }
            else
            {
                p_life += heal_v;
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
    public void Dead()
    {
        isLife = false;
        if (s_class == "Tree")
        {
            this.transform.GetComponent<EnvItemBe>().BeItem();
            Destroy(this.transform.parent.gameObject);
        }
        else if (s_class == "Monster")
        {
            this.transform.GetComponent<MonEnvBe>().BeItem();
            Destroy(this.transform.FindChild("AI").gameObject);
            this.transform.GetComponent<Animator>().SetBool("isLife", false);
        }
        else if(s_class=="Hero"&& s_Controler == "AI")
        {
            this.transform.GetComponent<Monster>().Dead();
            this.transform.GetComponent<Animator>().SetBool("isLife", false);
        }
        else if(s_class == "Hero" && s_Controler == "User")
        {
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
        TimeFly = Time.time;
    }
    private float TimeFly;
    private Vector3 initPos;
    void Update()
    {
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
        if (this.transform.GetComponent<CharacterController>().isGrounded)
        {
            TimeFly = Time.time;
        }
        else
        {
            if (Time.time - TimeFly > 3f)
            {
                if (s_class == "Hero" && s_Controler == "User")
                {
                    Dead();
                    this.transform.position = initPos;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}