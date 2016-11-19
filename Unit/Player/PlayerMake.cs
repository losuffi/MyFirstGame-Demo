using UnityEngine;
using System.Collections;

public class PlayerMake : MonoBehaviour {

    public void Work(int Itemid,string compose,int Item_count=1)
    {
        string[] val_arr = (string[])compose.Split('$').Clone();
        for (int k = 1; k < val_arr.Length; k++)
        {
            int id = int.Parse(val_arr[k].Split('%')[0]);
            int idCount = int.Parse(val_arr[k].Split('%')[1]);
            this.transform.GetComponent<player_pocket>().consume(id, idCount);
        }
        GameObject.Find("Environment").transform.FindChild("Item_class").GetComponent<Item_create>().ItemAloneCreate(this.transform.position+2*this.transform.forward, Itemid, Item_count);
    }
}
