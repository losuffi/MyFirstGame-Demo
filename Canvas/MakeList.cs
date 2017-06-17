using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GetSName;
public class MakeList : MonoBehaviour {
    public int StartItem;
    public int EndItem;
    public int AbleCount=0;
    public Transform ListModel;
    private string value="false";
    private bool isFirst=true;
    public void disp()
    { 
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        AbleCount = 0;
        for (int j = StartItem; j < EndItem + 1; j++)
        {
            value = this.transform.parent.parent.GetComponent<ComposeTable>().Query(j);
            if (value!="false")
            {
                string text=" ";
                int jcount;
                if (this.transform.FindChild("List_" + AbleCount) == null)
                {
                    Transform list = (Transform)Instantiate(ListModel, this.transform.position, this.transform.rotation,this.transform);
                    list.name = "List_" + AbleCount;
                    list.FindChild("ImageS").GetComponent<Image>().sprite = Item.ISprite(j);
                    string[] val_arr = (string[])value.Split('^')[1].Split('$').Clone();
                    for (int k = 1; k < val_arr.Length; k++)
                    {
                        int id = int.Parse(val_arr[k].Split('%')[0]);
                        int idCount = int.Parse(val_arr[k].Split('%')[1]);
                        text += "|" + Item.Name(id) + idCount + "个";
                    }
                    try
                    {
                        jcount = int.Parse(value.Split('^')[0]);
                    }
                    catch
                    {
                        jcount = 1;
                    }
                    list.FindChild("Text").GetComponent<Text>().text = text;
                    list.GetComponent<MakeListCurrent>().cplayer = this.transform.parent.parent.GetComponent<MakerSpace>().cplayer;
                    list.GetComponent<MakeListCurrent>().compose = value;
                    list.GetComponent<MakeListCurrent>().Item_id = j;
                    list.GetComponent<MakeListCurrent>().Item_count = jcount;
                    list.GetComponent<MakeListCurrent>().type = 1;
                    AbleCount++;
                }
            }
        }
    }
    public void dispAll()
    {
        if (isFirst)
        {
            GetList(true);
            GetList(false);
        }
    }
    public void disptable()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        AbleCount = 0;
        for (int j = StartItem; j < EndItem + 1; j++)
        {
            value = this.transform.parent.parent.GetComponent<ComposeTable>().Query_table(j);
            if (value != "false")
            {
                string text = " ";
                int jcount;
                if (this.transform.FindChild("List_" + AbleCount) == null)
                {
                    Transform list = (Transform)Instantiate(ListModel, this.transform.position, this.transform.rotation, this.transform);
                    list.name = "List_" + AbleCount;
                    list.FindChild("ImageS").GetComponent<Image>().sprite = Item.ISprite(j);
                    string[] val_arr = (string[])value.Split('^')[1].Split('$').Clone();
                    for (int k = 1; k < val_arr.Length; k++)
                    {
                        int id = int.Parse(val_arr[k].Split('%')[0]);
                        int idCount = int.Parse(val_arr[k].Split('%')[1]);
                        text += "|" + Item.Name(id) + idCount + "个";
                    }
                    try
                    {
                        jcount = int.Parse(value.Split('^')[0]);
                    }
                    catch
                    {
                        jcount = 1;
                    }
                    list.FindChild("Text").GetComponent<Text>().text = text;
                    list.GetComponent<MakeListCurrent>().cplayer = this.transform.parent.parent.GetComponent<MakerSpace>().cplayer;
                    list.GetComponent<MakeListCurrent>().compose = value;
                    list.GetComponent<MakeListCurrent>().Item_id = j;
                    list.GetComponent<MakeListCurrent>().Item_count = jcount;
                    list.GetComponent<MakeListCurrent>().type = 1;
                    AbleCount++;
                }
            }
        }
    }
    void GetList(bool isTable)
    {
        isFirst = false;
        AbleCount = 0;
        for (int j = StartItem; j < EndItem + 1; j++)
        {
            if(isTable)
                value = this.transform.parent.parent.GetComponent<ComposeTable>().GetList(j);
            else
                value = this.transform.parent.parent.GetComponent<ComposeTable>().GetList_table(j);
            if (value != "false")
            {
                string text = " ";
                int jcount;
                if (this.transform.FindChild("List_" + AbleCount) == null)
                {
                    Transform list = (Transform)Instantiate(ListModel, this.transform.position, this.transform.rotation, this.transform);
                    list.name = "List_" + AbleCount;
                    list.FindChild("ImageS").GetComponent<Image>().sprite = Item.ISprite(j);
                    string[] val_arr = (string[])value.Split('^')[1].Split('$').Clone();
                    for (int k = 1; k < val_arr.Length; k++)
                    {
                        int id = int.Parse(val_arr[k].Split('%')[0]);
                        int idCount = int.Parse(val_arr[k].Split('%')[1]);
                        text += "|" + Item.Name(id) + idCount + "个";
                    }
                    try
                    {
                        jcount = int.Parse(value.Split('^')[0]);
                    }
                    catch
                    {
                        jcount = 1;
                    }
                    list.FindChild("Text").GetComponent<Text>().text = text;
                    list.GetComponent<MakeListCurrent>().cplayer = this.transform.parent.parent.GetComponent<MakerSpace>().cplayer;
                    list.GetComponent<MakeListCurrent>().compose = value;
                    list.GetComponent<MakeListCurrent>().Item_id = j;
                    list.GetComponent<MakeListCurrent>().Item_count = jcount;
                    list.GetComponent<MakeListCurrent>().type = 2;
                    AbleCount++;
                }
            }
        }
    }
}
