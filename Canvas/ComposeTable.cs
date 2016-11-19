using UnityEngine;
using System.Collections;
public class ComposeTable : MonoBehaviour {
    public Hashtable Com_data = new Hashtable();
    public Transform cplayer;
    public void Com_Init()
    {
        cplayer = this.transform.GetComponent<MakerSpace>().cplayer;
        Com_data.Add(20006, "$20014%1$20003%1$20004%1");
        Com_data.Add(20005, "$20003%4");
        Com_data.Add(20013, "$20004%1$20003%1");
        Com_data.Add(20007, "$20014%2$20006%1$20013%2");
        Com_data.Add(20010, "$20001%2$20013%1$20015%1");
        Com_data.Add(20011, "$20002%2$20013%1$20015%1");
        Com_data.Add(20016, "$20004%3$20003%2");
        Com_data.Add(20017, "$20002%2$20003%1$20004%1");
    }
    public string Query(int Item_id)
    {
        if (Com_data.Contains(Item_id))
        {
            string value = (string)Com_data[Item_id];
            string[] val_arr=(string[])value.Split('$').Clone();
            for(int k = 1; k < val_arr.Length; k++)
            {
                string res = val_arr[k];
                int id=int.Parse(res.Split('%')[0]);
                int idCount= int.Parse(res.Split('%')[1]);
                if (cplayer.GetComponent<player_pocket>().Query(id) < idCount)
                {
                    return "false";
                }
            }
            return value;
        }
        else
        {
            return "false";
        }
    }
    public string GetList(int Item_id)
    {
        if (Com_data.Contains(Item_id))
        {
            string value = (string)Com_data[Item_id];
            string[] val_arr = (string[])value.Split('$').Clone();
            return value;
        }
        else
        {
            return "false";
        }
    }
}
