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
        Transform TimeTram = GameObject.Find("System").transform.FindChild("TIme");
        int _time = TimeTram.GetComponent<TimeTram>().Time;
        int _day = TimeTram.GetComponent<TimeTram>().Day;
        cplayer.GetComponent<Self_class>().s_Scout += 0.1f * (_day * 14400 + _time - 32f);
        string scout = ((int)cplayer.GetComponent<Self_class>().s_Scout).ToString();
        this.transform.FindChild("Board").FindChild("T").GetComponent<Text>().text =scout;
    }
}
