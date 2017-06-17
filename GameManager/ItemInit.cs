using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ItemInit : NetworkBehaviour {
    private bool isSet = false;
    void Init()
    {
        transform.tag = "Item";
        transform.parent = GameObject.Find("Environment").transform.FindChild("Item_class");
        int sid = transform.GetComponent<Self_class>().s_id;
        transform.GetComponent<Self_class>().s_name = transform.parent.FindChild("get_sName").GetComponent<get_sName>().Item_name(sid);
        transform.GetComponent<Self_class>().s_Icontent = transform.parent.FindChild("get_sName").GetComponent<get_sName>().Item_content(sid);
        transform.GetComponent<Self_class>().s_iType = transform.parent.FindChild("get_sName").GetComponent<get_sName>().Item_type(sid);
        transform.GetComponent<Self_class>().isTwiceItem = transform.parent.FindChild("get_sName").GetComponent<get_sName>().Item_IsTwiceCast(sid);
        this.gameObject.name = transform.GetComponent<Self_class>().s_id + "Item" + netId;
        isSet = true;
    }
    void Update()
    {
        if (!isSet)
        {
            if (transform.GetComponent<Self_class>().s_id > 20000)
            {
                Init();
            }
        }
    }
    public void Pick(GameObject pickMan)
    {
        if (!isServer) return;
        this.GetComponent<Self_class>().isPick = true;
        RpcPlayerPick(pickMan);
    }
    [ClientRpc]void RpcPlayerPick(GameObject pickMan)
    {
        GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        if (locplayer == pickMan)
        {
            locplayer.GetComponent<player_pocket>().Pick(this.transform);
        }
    }
}
