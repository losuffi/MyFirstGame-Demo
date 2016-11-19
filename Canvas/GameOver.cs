using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {
    public Transform cplayer;
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void GetScout()
    {
        Transform TimeTram = GameObject.Find("System").transform;
        int Days = TimeTram.GetComponent<TimeTram>().DayCount;
        int Hour = TimeTram.GetComponent<TimeTram>().NowTime[0];
        int Minu = TimeTram.GetComponent<TimeTram>().NowTime[1];
        cplayer.GetComponent<Self_class>().s_Scout += 0.1f * (Days * 24 * 60 + Hour * 60 + Minu - 32f);
        string scout = ((int)cplayer.GetComponent<Self_class>().s_Scout).ToString();
        this.transform.FindChild("Board").FindChild("T").GetComponent<Text>().text =scout;
    }
}
