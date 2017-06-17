using UnityEngine;
using System.Collections;
public class TimeTram : MonoBehaviour {
    [SerializeField]
    int _time;
    public int Time
    {
        get
        {
            return _time;
        }
        set
        {
            _time = value;
            TimeSet();
            WorkSky();
        }
    }
    public int Hour;
    public int Minute;
    public int Day;
    public int Speed;
    [System.Serializable]
    public struct cl
    {
        public Color day;
        public Color night;
        public Color mornig;
        public Color dusk;
    }
    public cl LightColor;
    void TimeSet()
    {
        Hour = Time / 600;
        Minute = Time % 600;
    }
    void WorkSky()
    {
        this.transform.parent.Find("Directional light").eulerAngles = new Vector3((Time-3600)*0.025f, -130, 0);
        if (Time >= 2700 && Time < 11700)
        {
            this.transform.parent.Find("Directional light").GetComponent<Light>().intensity = 1 + 0.4f * Mathf.Sin((Time - 3600) * 0.025f * (Mathf.PI / 360));
        }
        else
        {
            this.transform.parent.Find("Directional light").GetComponent<Light>().intensity = 0;
        }
        if (Time >= 2700 && Time < 4500)
        {
            RenderSettings.ambientLight = LightColor.mornig;
        }
        else if (Time >= 4500 && Time < 9900)
        {
            RenderSettings.ambientLight = LightColor.day;
        }
        else if (Time >= 9900 && Time < 11700)
        {
            RenderSettings.ambientLight = LightColor.dusk;
        }
        else
        {
            RenderSettings.ambientLight = LightColor.night;
        }
    }
    void Start()
    {
        StartCoroutine(runtime());
    }
    IEnumerator runtime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Time >= 14399)
            {
                Day++;
                Time = 0;
            }
            Time += Speed;
        }
    }
}
