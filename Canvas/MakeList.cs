using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
            if (this.transform.GetChild(i).name != "get_sName")
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
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
                    list.FindChild("ImageS").GetComponent<Image>().sprite = this.transform.parent.parent.FindChild("Sprite").GetComponent<ImageSprite>().GetSprite(j);
                    string[] val_arr = (string[])value.Split('$').Clone();
                    for (int k = 1; k < val_arr.Length; k++)
                    {
                        int id = int.Parse(val_arr[k].Split('%')[0]);
                        int idCount = int.Parse(val_arr[k].Split('%')[1]);
                        text += this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_name(id) + idCount + "个+";
                    }
                    try
                    {
                        jcount = int.Parse(value.Split('#')[1]);
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
            isFirst = false;
            AbleCount = 0;
            for (int j = StartItem; j < EndItem + 1; j++)
            {
                value = this.transform.parent.parent.GetComponent<ComposeTable>().GetList(j);
                if (value != "false")
                {
                    string text = " ";
                    int jcount;
                    if (this.transform.FindChild("List_" + AbleCount) == null)
                    {
                        Transform list = (Transform)Instantiate(ListModel, this.transform.position, this.transform.rotation, this.transform);
                        list.name = "List_" + AbleCount;
                        list.FindChild("ImageS").GetComponent<Image>().sprite = this.transform.parent.parent.FindChild("Sprite").GetComponent<ImageSprite>().GetSprite(j);
                        string[] val_arr = (string[])value.Split('$').Clone();
                        for (int k = 1; k < val_arr.Length; k++)
                        {
                            int id = int.Parse(val_arr[k].Split('%')[0]);
                            int idCount = int.Parse(val_arr[k].Split('%')[1]);
                            text += this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_name(id) + idCount + "个+";
                        }
                        try
                        {
                            jcount = int.Parse(value.Split('#')[1]);
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
}
