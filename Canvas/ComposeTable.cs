using UnityEngine;
using System.Collections;
public class ComposeTable : MonoBehaviour {
    public Hashtable Com_data = new Hashtable();
    public Hashtable Com_data_table = new Hashtable();
    public Transform cplayer;
    /// <summary>
    /// 尽量改成读取数据型的 合成库
    /// </summary>
    public void Com_Init()
    {
        cplayer = this.transform.GetComponent<MakerSpace>().cplayer;
        Com_data.Add(20022, "1^$20003%4");
        Com_data.Add(20018, "1^$20022%2$20015%2$20021%1");
        Com_data.Add(20023, "1^$20022%2$20015%2$20021%1");
        Com_data.Add(20024, "1^$20001%6$20002%2$20015%3$20022%4");
        Com_data.Add(20025, "1^$20001%4$20002%4");
        Com_data.Add(20026, "6^$20001%1$20003%3");
        Com_Init_table();
    }
    public void Com_Init_table()
    {
        Com_data_table.Add(20013, "4^$20004%2");
        Com_data_table.Add(20006, "1^$20021%1$20013%2$20022%1$20004%1");
    }
    public string Query(int Item_id)
    {
        if (Com_data.Contains(Item_id))
        {
            string str = (string)Com_data[Item_id];
            string value = str.Split('^')[1];
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
            return str;
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
            return value;
        }
        else
        {
            return "false";
        }
    }
    public string Query_table(int Item_id)
    {
        if (Com_data_table.Contains(Item_id))
        {
            string str = (string)Com_data_table[Item_id];
            string value = str.Split('^')[1];
            string[] val_arr = (string[])value.Split('$').Clone();
            for (int k = 1; k < val_arr.Length; k++)
            {
                string res = val_arr[k];
                int id = int.Parse(res.Split('%')[0]);
                int idCount = int.Parse(res.Split('%')[1]);
                if (cplayer.GetComponent<player_pocket>().Query(id) < idCount)
                {
                    return "false";
                }
            }
            return str;
        }
        else
        {
            return "false";
        }
    }
    public string GetList_table(int Item_id)
    {
        if (Com_data_table.Contains(Item_id))
        {
            string value = (string)Com_data_table[Item_id];
            return value;
        }
        else
        {
            return "false";
        }
    }
}
