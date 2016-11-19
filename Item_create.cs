using UnityEngine;
using System.Collections;

public class Item_create : MonoBehaviour {

    // Use this for initialization
    public Transform Item;
    private Vector3 Item_pos;
    private Transform clone;
    public int RandItemCount;
    public int MaxItemCount;
	void Start() {
        RandItemCount = 0;
        MaxItemCount = 30;
        test(20005, 20008);
    }
    void test(int start,int end)
    {
        for(int k = start; k < end + 1; k++)
        {
            float pos_x = Random.Range(2, 5);
            float pos_z = Random.Range(2, 5);
            float pos_y = Terrain.activeTerrain.SampleHeight(new Vector3(pos_x, 1, pos_z)) + 2;
            Item_pos = new Vector3(pos_x, pos_y, pos_z);
            clone = (Transform)Instantiate(Item, Item_pos, Quaternion.identity, this.transform);
            clone.tag = "Item";
            clone.gameObject.GetComponent<Self_class>().s_id = k;
            clone.gameObject.GetComponent<Self_class>().s_name = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_name(k);
            clone.gameObject.GetComponent<Self_class>().s_Icontent = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_content(k);
            clone.gameObject.GetComponent<Self_class>().s_iType = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_type(k);
            clone.gameObject.GetComponent<Self_class>().isTwiceItem = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_IsTwiceCast(k);
        }
    }
    void Item_pos_c()
    {
        float pos_x = Random.Range(2, 55);
        float pos_z = Random.Range(2, 55);
        float pos_y = Terrain.activeTerrain.SampleHeight(new Vector3(pos_x, 1, pos_z))+2;
        Item_pos = new Vector3(pos_x, pos_y, pos_z);
    }
    void FixedUpdate()            //0.02 秒一帧
    {
    }
    public void RandCreate()
    {
        for (int i = 0; i < MaxItemCount - RandItemCount; i++)
        {
            Item_pos_c();
            int rnd = ((int)Random.Range(0, 4)) + 20001;
            clone = (Transform)Instantiate(Item, Item_pos, Quaternion.identity, this.transform);
            clone.tag = "Item";
            clone.gameObject.GetComponent<Self_class>().s_id = rnd;
            clone.gameObject.GetComponent<Self_class>().s_name = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_name(rnd);
            clone.gameObject.GetComponent<Self_class>().s_Icontent = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_content(rnd);
            clone.gameObject.GetComponent<Self_class>().s_iType = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_type(rnd);
            clone.gameObject.GetComponent<Self_class>().isTwiceItem = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_IsTwiceCast(rnd);
        }
        RandItemCount=MaxItemCount;
    }
    public void ItemAloneCreate(Vector3 position,int Item_id,int Item_count=1)
    {
        float pos_x = position.x;
        float pos_z = position.z;
        float pos_y = Terrain.activeTerrain.SampleHeight(new Vector3(pos_x, 1, pos_z)) + 2;
        position = new Vector3(pos_x, pos_y, pos_z);
        clone = (Transform)Instantiate(Item, position, Quaternion.identity, this.transform);
        clone.tag = "Item";
        clone.gameObject.GetComponent<Self_class>().s_id = Item_id;
        clone.gameObject.GetComponent<Self_class>().s_iCount = Item_count;
        clone.gameObject.GetComponent<Self_class>().s_name = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_name(Item_id);
        clone.gameObject.GetComponent<Self_class>().s_Icontent = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_content(Item_id);
        clone.gameObject.GetComponent<Self_class>().s_iType = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_type(Item_id);
        clone.gameObject.GetComponent<Self_class>().isTwiceItem = this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_IsTwiceCast(Item_id);
        RandItemCount++;
    }
}
