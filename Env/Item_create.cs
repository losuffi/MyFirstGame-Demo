using UnityEngine;
using System.Collections;
public class Item_create : MonoBehaviour {
    public GameObject Item;
    public int RandItemCount;
    public int MaxItemCount;
	void Start() {
        RandItemCount = 0;
        MaxItemCount = 30;
    }
    public void ItemAloneCreate(Vector3 position,int Item_id,int Item_count=1)
    {
        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdItemCreate(Item_id, Item_count, position);
        RandItemCount++;
    }
}
