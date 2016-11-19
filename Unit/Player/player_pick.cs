using UnityEngine;
using System.Collections;

public class player_pick : MonoBehaviour {
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Self_class>()!=null&& other.gameObject.GetComponent<Self_class>().s_class == "Hero" && other.gameObject.GetComponent<Self_class>().isLife)
        {
            Transform Item = this.transform;
            if (Item.GetComponent<Self_class>().s_iType == "Material")
            {
                this.transform.parent.GetComponent<Item_create>().RandItemCount--;
            }
            if (other.gameObject.GetComponent<player_pocket>() != null)
            {
                other.gameObject.GetComponent<player_pocket>().Pick(Item);
            }
            else
            {
                Destroy(this.transform.gameObject);
            }
        }
    }
}
