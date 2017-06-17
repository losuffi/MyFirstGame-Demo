using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class EffectInit : NetworkBehaviour {
    [SyncVar]
    public string effctname="None";
    [SyncVar]
    public GameObject BindUnit;
    [SyncVar]
    public Vector3 BindPos;
    [SyncVar]
    public bool isBindP = false;
    public float Time = 0;
    private bool isSet=false;
    void Start()
    {
        StartCoroutine(runtime());
    }
    IEnumerator runtime()
    {
        while (!isSet)
        {
            if (BindUnit != null)
            {
                transform.parent = BindUnit.transform;
                transform.rotation = BindUnit.transform.rotation;
                transform.position = BindUnit.transform.position + new Vector3(0, 1, 0);
                transform.name = effctname;
                isSet = true;
                yield return 0;
            }
            else if (isBindP)
            {
                transform.parent = GameObject.Find("Hero").transform.FindChild("Effect");
                transform.rotation = Quaternion.identity;
                transform.position = BindPos;
                transform.name = effctname;
                isSet = true;
                if (Time > 0)
                    Destroy(this.gameObject, Time);
                yield return 0;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void BindU(GameObject obj,int efindex)
    {
        BindUnit = obj;
        effctname = "BU" + efindex;
    }
    public void BindP(Vector3 pos,int efindex)
    {
        BindPos = pos;
        isBindP = true;
        effctname = "BV" + efindex;
    }
}
