using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    private bool IsCanBuild;
    private Transform Scaner;
    public int SetId;
    void Awake()
    {
        transform.GetComponent<Renderer>().material.color = Color.green;
        Scaner = transform.FindChild("Scan");
    }
    public void FalsePos()
    {
        transform.GetComponent<Renderer>().material.color = Color.red;
        IsCanBuild = false;
    }
    public void TruePos()
    {
        transform.GetComponent<Renderer>().material.color = Color.green;
        IsCanBuild = true;
    }
    public bool IsTrueBuild()
    {
        if (IsCanBuild)
        {
            return true;
        }else
        {
            return false;
        }
    }
    private int angle = 0;
    private int flag = 0;
    void FixedUpdate()
    {
        RaycastHit hit;
        if (angle > 8)
        {
            angle = 0;
            if (flag != 1)
            {
                flag = 0;
                TruePos();
            }
            else
            {
                flag = 2;
            }
        }
        else
        {
            for (int j = 0; j < 9; j++)
            {
                Scaner.Rotate(Scaner.up * 5);
                if (Physics.Raycast(Scaner.position, Scaner.forward, out hit, 1.4f))
                {
                    flag = 1;
                    FalsePos();
                }
            }
            angle++;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,1+Terrain.activeTerrain.SampleHeight(transform.position), transform.position.z), Time.fixedDeltaTime * 12);
    }
    public void SetBuildId(int x)
    {
        SetId = x;
    }
    public void SetBuild(GameObject Owner)
    {
        int j = SetId - 60001;
        if (j >= 0)
        {
            GameObject lplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
            lplayer.GetComponent<UnitSyncCmd>().CmdBuilding(j, transform.position-0.6f*transform.up, Quaternion.identity, GameObject.Find("Hero").transform.FindChild("BuildingForPlayer").gameObject, Owner);
        }
        GameObject.Destroy(this.gameObject);
    }
}
