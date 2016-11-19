using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PersonMsgBoard : MonoBehaviour {
    public Transform cplayer;
    private float speed, defense, attack, scout, slife, hunger, hungerspeed, gather;
    private int freetalent;
    private string sname;
    private Transform Tname, Tdefense, Tattack, Tscout, Tslife, Thunger, Thungerspeed, Tgather, Tfreetalent, Tspeed;
    void Awake()
    {
        Tname = this.transform.FindChild("Tname");
        Tdefense = this.transform.FindChild("Tdefense");
        Tattack = this.transform.FindChild("Tattack");
        Tscout = this.transform.FindChild("Tscout");
        Tslife = this.transform.FindChild("Tslife");
        Thunger = this.transform.FindChild("Thunger");
        Thungerspeed = this.transform.FindChild("Thungerspeed");
        Tgather = this.transform.FindChild("Tgather");
        Tfreetalent = this.transform.FindChild("Tfreetalent");
        Tspeed = this.transform.FindChild("Tspeed");
    }
    public void Work()
    {
        GetData();
        Tname.GetComponent<Text>().text = sname;
        Tdefense.GetComponent<Text>().text = defense.ToString();
        Tattack.GetComponent<Text>().text = attack.ToString();
        Tscout.GetComponent<Text>().text = scout.ToString();
        Tslife.GetComponent<Text>().text = slife.ToString();
        Thunger.GetComponent<Text>().text = hunger.ToString();
        Thungerspeed.GetComponent<Text>().text = hungerspeed.ToString();
        Tgather.GetComponent<Text>().text = gather.ToString();
        Tfreetalent.GetComponent<Text>().text = freetalent.ToString();
        Tspeed.GetComponent<Text>().text = speed.ToString();
    }
    void GetData()
    {
        Self_class sc = cplayer.GetComponent<Self_class>();
        speed = sc.s_speed;
        defense = sc.s_Defence;
        attack = sc.s_AttackValue;
        scout = sc.s_Scout;
        slife = sc.s_life;
        hunger = sc.s_Hunger;
        hungerspeed = sc.s_HungerSpeed;
        gather = sc.s_GatherValue;
        freetalent = sc.TalentFreePoint;
        sname = sc.s_name;
        Transform TimeTram = GameObject.Find("System").transform;
        int Days = TimeTram.GetComponent<TimeTram>().DayCount;
        int Hour = TimeTram.GetComponent<TimeTram>().NowTime[0];
        int Minu = TimeTram.GetComponent<TimeTram>().NowTime[1];
        scout+=0.1f * (Days * 24 * 60 + Hour * 60 + Minu - 32f);
    }
}
