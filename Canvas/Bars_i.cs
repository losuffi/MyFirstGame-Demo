using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Bars_i : MonoBehaviour {
    public Transform cplayer;
    private float timel;
    private bool isWorking;
    private float Tracks;
    private int timeCount;
    private float value;
    private bool isEnd;
	void Start() {
        isEnd = true;
        isWorking = false;
	}
    public void work(float _Tracks)
    {
        Tracks = _Tracks;
        timel = Time.time;
        this.transform.FindChild("Bars").gameObject.SetActive(true);
        isWorking = true;
        isEnd = false;
        timeCount = 0;
        value = 0;
    }
    void FixedUpdate()
    {
        if (isWorking&&!isEnd)
        {
            if (Time.time - timel < Tracks)
            {
                value = (Time.time - timel) / Tracks;
                this.transform.FindChild("Bars").GetComponent<Scrollbar>().size = value;
            }
            else
            {
                timel = Time.time;
                timeCount++;
            }
        }
    }
    public float End()
    {
        isEnd = true;
        this.transform.FindChild("Bars").gameObject.SetActive(false);
        return value;
    }
}
