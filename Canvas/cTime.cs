using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class cTime : MonoBehaviour {
    private int Hour;
    private int Minute;
    private GameObject TIme;
    void Awake()
    {
        TIme = GameObject.Find("System").transform.FindChild("TIme").gameObject;
    }
    void FixedUpdate()
    {
        Hour = TIme.GetComponent<TimeTram>().Hour;
        Minute = TIme.GetComponent<TimeTram>().Minute;
        this.transform.FindChild("Text").GetComponent<Text>().text = Hour + ":" + (Minute / 10);
    }
}
