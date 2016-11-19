using UnityEngine;
using System.Collections;

public class player_pocket : MonoBehaviour {
    private bool isCover;
    public Transform[] tItem_temp=new Transform[9];
    public Transform Model;
	void Start() {
        isCover = false;
	}
    public  int Query(int id)
    {
        for(int k = 0; k < 9; k++)
        {
            if (tItem_temp[k] != null)
            {
                if (tItem_temp[k].GetComponent<Self_class>().s_id == id)
                {
                    return tItem_temp[k].GetComponent<Self_class>().s_iCount;
                }
            }
        }
        return 0;
    }
    bool findadr(Transform Item_t)
    {
        isCover = false;
        for(int k = 0; k < tItem_temp.Length; k++)
        {
            if (tItem_temp[k] != null)
            {
                if (tItem_temp[k].GetComponent<Self_class>().s_id == Item_t.GetComponent<Self_class>().s_id)
                {
                    tItem_temp[k].GetComponent<Self_class>().s_iCount++;
                    Destroy(Item_t.gameObject);
                    isCover = true;
                    return true;
                }
            }         
        }
        if (!isCover)
        {
            for (int k = 0; k < tItem_temp.Length; k++)
            {
                if (tItem_temp[k] == null)
                { 
                    tItem_temp[k] = Item_t;
                    Item_t.name = "Item" + k;
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    void Canvas_con()
    {
        GameObject.Find("Player_CAnvas").transform.FindChild("B_poc").gameObject.GetComponent<Pocket>().Update_canvas_con(tItem_temp);
    }
    public void Pick(Transform Item)
    {
        Transform I_temp = (Transform)Instantiate(Model, this.transform.position, this.transform.rotation);
        I_temp.parent = this.transform;
        I_temp.GetComponent<Self_class>().s_class = Item.GetComponent<Self_class>().s_class;
        I_temp.GetComponent<Self_class>().s_Icontent = Item.GetComponent<Self_class>().s_Icontent;
        I_temp.GetComponent<Self_class>().s_iType = Item.GetComponent<Self_class>().s_iType;
        I_temp.GetComponent<Self_class>().s_name = Item.GetComponent<Self_class>().s_name;
        I_temp.GetComponent<Self_class>().s_id = Item.GetComponent<Self_class>().s_id;
        I_temp.GetComponent<Self_class>().s_iCount = Item.GetComponent<Self_class>().s_iCount;
        I_temp.GetComponent<Self_class>().isTwiceItem = Item.GetComponent<Self_class>().isTwiceItem;
        try
        {
            this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis("获取物品：" + Item.GetComponent<Self_class>().s_name + Item.GetComponent<Self_class>().s_iCount + "个");
        }
        catch { }
        Destroy(Item.gameObject);
        if (!findadr(I_temp))
        {
            try
            {
                this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis("背包已满！");
            }
            catch { }
        }
        Canvas_con();
    }
    public void drop(int Item_addr)
    {
        Destroy(tItem_temp[Item_addr].gameObject);
        tItem_temp[Item_addr] = null;
        Canvas_con();
    }
    public void use(int Item_addr)
    {
        int spell_id = this.transform.parent.FindChild("get_sName").GetComponent<get_sName>().Item_Spell(tItem_temp[Item_addr].GetComponent<Self_class>().s_id);
        string note = "使用了"+tItem_temp[Item_addr].GetComponent<Self_class>().s_name;
        this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis(note);
        this.transform.parent.FindChild("Spell").GetComponent<Spell_cast>().casting(spell_id, this.transform);
        if (tItem_temp[Item_addr].GetComponent<Self_class>().s_iType == "prop"|| tItem_temp[Item_addr].GetComponent<Self_class>().s_iType == "staticequi")
        {
            if (tItem_temp[Item_addr].GetComponent<Self_class>().isTwiceItem)
            {
                if (tItem_temp[Item_addr].GetComponent<Self_class>().isCast)
                {
                    tItem_temp[Item_addr].GetComponent<Self_class>().s_iCount--;
                }
                else
                {
                    tItem_temp[Item_addr].GetComponent<Self_class>().isCast = true;
                }
            }
            else
            {
                if (tItem_temp[Item_addr].GetComponent<Self_class>().s_iType == "staticequi")
                {
                    this.transform.parent.FindChild("Talent").GetComponent<TalentCenter>().EquiWork(tItem_temp[Item_addr].GetComponent<Self_class>().s_id, this.transform);
                }
                tItem_temp[Item_addr].GetComponent<Self_class>().s_iCount--;
            }
            if (tItem_temp[Item_addr].GetComponent<Self_class>().s_iCount == 0)
            {
                this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("Pocket").FindChild("Pocket_dashboard").GetComponent<Pocket_Dashboard>().dash_addr = Item_addr + 1;
                this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("Pocket").FindChild("Pocket_dashboard").GetComponent<Pocket_Dashboard>().drop();
            }
        }
    }
    public void consume(int Item_id,int c_count)
    {
        for (int k = 0; k < 9; k++)
        {
            if (tItem_temp[k] != null)
            {
                if (tItem_temp[k].GetComponent<Self_class>().s_id == Item_id)
                {
                    if (tItem_temp[k].GetComponent<Self_class>().s_iCount - c_count > 0)
                    {
                        tItem_temp[k].GetComponent<Self_class>().s_iCount -= c_count;
                        Canvas_con();
                    }
                    else
                    {
                        drop(k);
                    }
                }
            }
        }
    }
}
