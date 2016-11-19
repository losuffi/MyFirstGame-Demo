using UnityEngine;
using System.Collections;
public class TimeTram : MonoBehaviour {
    public int[] StartTime=new int[2];
    public int[] NowTime = new int[2];
    public int DayCount=0;
    public Material[] skeybox;
    private bool isNight;
    private float delay;
    public Light nlight;
    private float delpos;
    private float desid;
    void Start()
    {
        NowTime = (int[])StartTime.Clone();
        RenderSettings.skybox = skeybox[0];
        isNight = ((NowTime[0]>18||NowTime[0]<6) ? true : false);
        delay = Time.time;
        delpos = 0.25f;
        desid = 0;
        nlight.GetComponent<Light>().intensity = 1;
    }
    void TramSky()
    {
        if (NowTime[0] >= 6 && NowTime[0] <= 18)
        {
            RenderSettings.skybox = skeybox[(NowTime[0]-6)/3];
        }
        else
        {
            isNight = true;
            RenderSettings.skybox = skeybox[4];
        }
    }
    void TramLight()
    {
        float A = 3.6f;
        if (NowTime[1] % 5 == 0&&!isNight)
        {
            desid ++;
            nlight.GetComponent<Light>().intensity =A * Mathf.Sin(desid * Mathf.PI / 168);
        }
        else if(isNight)
        {
            desid = 0;
        }
        nlight.transform.Rotate(-nlight.transform.right *delpos);
    }
    void FixedUpdate()
    {
        if (Time.time - delay > 1)
        {
            delay = Time.time;
            if (NowTime[1] >= 59)
            {
                NowTime[1] = 0;
                NowTime[0] += 1;
            }
            else
            {
                NowTime[1] += 1;
            }
            if (NowTime[0] > 23)
            {
                NowTime[0] = 0;
                DayCount++;
            }
            if (NowTime[0] == 5 && NowTime[1] == 58)
            {
                isNight = false;
            }
            if (NowTime[1] == 3 && NowTime[0] % 3 == 0 && !isNight)
            {
                TramSky();
            }
            TramLight();
        }
        //this.transform.FindChild("uSkyPro").GetComponent<uSkyManager>().Timeline = NowTime[0] + NowTime[1] / 60;
    }
}
