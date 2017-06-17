using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class BonFire : NetworkBehaviour {
    [SyncVar]
    public int FuelCount=0;
    private GameObject FireLight;
    private float DurationTimel, StartTime;
    public float FireTime;
    private bool IsHaveFuel;
    void Awake()
    {
        FireLight = transform.FindChild("FireLight").gameObject;
        StartTime = Time.time;
        IsHaveFuel = true;
    }
    void Start()
    {
        Light();
    }
    void Fire()
    {
        if (FuelCount > 0)
        {
            CmdSubFuel();
        }
        else
        {
            IsHaveFuel = false;
            DieOut();
            CmdZerFuel();
        }
    }
    void DieOut()
    {
        GameObject lplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        lplayer.GetComponent<UnitSyncCmd>().CmdDes(FireLight.transform.FindChild("Fire").gameObject);
    }
    void Light()
    {
        IsHaveFuel = true;
        StartTime = Time.time;
        GameObject lplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        lplayer.GetComponent<UnitSyncCmd>().CmdInsSth(0, transform.position, Quaternion.identity, this.gameObject);
    }
    void FixedUpdate()
    {
        if (IsHaveFuel&&isServer)
        {
            if (Time.time - StartTime >= FireTime)
            {
                Fire();
                StartTime = Time.time;
            }
        }
    }
    public void AddFuel()
    {
        if (!IsHaveFuel)
        {
            Light();
        }
        CmdAddFuel();
    }
    [Command]void CmdAddFuel()
    {
        FuelCount++;
    }
    [Command]void CmdSubFuel()
    {
        FuelCount--;
    }
    [Command]void CmdZerFuel()
    {
        FuelCount = 0;
    }
}
