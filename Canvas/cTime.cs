using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class cTime : MonoBehaviour {
    private int Hour;
    private int Minute;
    void FixedUpdate()
    {
        Hour = GameObject.Find("System").GetComponent<TimeTram>().NowTime[0];
        Minute = GameObject.Find("System").GetComponent<TimeTram>().NowTime[1];
        this.transform.FindChild("Text").GetComponent<Text>().text = Hour + ":" + Minute;
    }
}
