using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerPcControl : MonoBehaviour {
    public bool isOpen = false;
    private float overtime = 0.5f;
    private float starttime;
    private int frame=0;
    private float fps;
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            isOpen = true;
        }
        starttime = Time.realtimeSinceStartup;
    }
    void Update()
    {
        ++frame;
        if (!isOpen)
        {
            return;
        }
        if (Time.realtimeSinceStartup > starttime + overtime)
        {
            fps = (float)(frame / (Time.realtimeSinceStartup - starttime));
            frame = 0;
            starttime = Time.realtimeSinceStartup;
            GameObject.Find("Canvas").transform.FindChild("Test").GetComponent<Text>().text = "FPS:" + fps.ToString("F2");
        }
    }
}
