using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Excre_class : NetworkBehaviour {
    [SyncVar]
    public Transform ownner;
    public float LiveTime;
    public float StartTime;
    [SyncVar]
    public string Type;
    [SyncVar]
    public string Name;
    [SyncVar]
    public float ExtraVal;
    void Start()
    {
        StartTime = Time.time;
        transform.parent = GameObject.Find("Hero").transform.FindChild("Excrement");
    }
    void FixedUpdate()
    {
        if (!isServer) return;
        if (isServer &&Time.time - StartTime >= LiveTime)
        {
            Des();
        }
    }
    public void Des()
    {
        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdDes(this.gameObject);
    }
}
