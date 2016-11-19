using UnityEngine;
using System.Collections;

public class Excre_class : MonoBehaviour {
    public Transform ownner;
    public float LiveTime;
    public float StartTime;
    public string Type;
    public string Name;
    void Start()
    {
        StartTime = Time.time;
    }
    void FixedUpdate()
    {
        if (Time.time - StartTime >= LiveTime)
        {
            Des();
        }
    }
    public void Des()
    {
        Destroy(this.gameObject);
    }
}
