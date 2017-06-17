using UnityEngine;
using System.Collections;

public class EnvItemBe : MonoBehaviour {
    public float[] probability;
    //public int[] ItemIdHigher=0;
    public int[] ItemId;
    private Transform ItemCenter;
    void Awake()
    {
        ItemCenter = GameObject.Find("Environment").transform.FindChild("Item_class");
    }
    public void BeItem()
    {
        for(int j = 0; j < ItemId.Length; j++)
        {
            if (probability[j] - Random.Range(0, 99.9f) >= 0)
            {
                ItemCenter.GetComponent<Item_create>().ItemAloneCreate(this.transform.position, ItemId[j]);
            }
        }
    }
}
