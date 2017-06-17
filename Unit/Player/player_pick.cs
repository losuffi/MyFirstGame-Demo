using UnityEngine;
using System.Collections;

public class player_pick : MonoBehaviour {
    void OnTriggerStay(Collider other)
    {
        if (!this.GetComponent<Self_class>().isPick &&other.gameObject.GetComponent<Self_class>()!=null && other.gameObject.GetComponent<Self_class>().s_class == "Hero" && other.gameObject.GetComponent<Self_class>().isLife)
        {
            //if (transform.GetComponent<Self_class>().s_iType == "Material")
            //{
            //    this.transform.parent.GetComponent<Item_create>().RandItemCount--;
            //}
            if (other.gameObject.GetComponent<player_pocket>() != null)
            {
                this.GetComponent<ItemInit>().Pick(other.gameObject);
            }
            else
            {
                GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdDes(this.gameObject);
            }
        }
    }
}
