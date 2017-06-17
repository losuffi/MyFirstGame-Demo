using UnityEngine;
using System.Collections;
using _spellLib;
using GetSName;
using UnityEngine.Networking;
public class player_pocket : NetworkBehaviour
{
    bool IsShow;
    public Bag mybag = new Bag(9);
    void Start() {
        mybag.SetOwner(this.gameObject);
        IsShow = false;
	}
    public  int Query(int id)
    {
        return mybag.GetCount(id);
    }
    public void Canvas_con()
    {
        this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("Center").gameObject.GetComponent<Pocket>().Update_canvas_con();
    }
    public void Pick(Transform Item)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (mybag.IsFull())
        {
            GetComponent<UnitSyncCmd>().CmdSetPick(Item.gameObject);
            if(!IsShow) MyCanvas.IssueDis("背包已满！");
            IsShow = true;
        }
        else
        {
            MyCanvas.IssueDis("得到：" + Item.GetComponent<Self_class>().s_name + " " + Item.GetComponent<Self_class>().s_iCount + "个");
            mybag.Push(Item.gameObject);
            GetComponent<UnitSyncCmd>().CmdDes(Item.gameObject);
            Canvas_con();
            IsShow = false;
        }
    }
    public void dropall()
    {
        mybag.Clear();
        Canvas_con();
    }
    public void drop(int Item_sid)
    {
        mybag.Delete(Item_sid);
        Canvas_con();
    }
    public void use(int Item_sid)
    {
        if (!mybag.Query(Item_sid))
        {
            MyCanvas.IssueDis("该物品，已缺失");
            return;
        }
        int spell_id = Item.SpellID(Item_sid);
        string note = "使用了"+Item.Name(Item_sid);
        MyCanvas.IssueDis(note);
        SpellSystem.SpellCast(spell_id, this.gameObject);
        if (Item.Type(Item_sid) == "prop"|| Item.Type(Item_sid) == "staticequi"|| Item.Type(Item_sid) == "Building") 
        {
            if (Item.IsCanTwiceSpell(Item_sid))
            {
                mybag.Cast(Item_sid);
                Canvas_con();
            }
            else
            {
                if (Item.Type(Item_sid) == "staticequi")
                {
                    Equi(Item_sid);
                }
                else if(Item.Type(Item_sid) == "Building")
                {
                    GameObject temp = mybag.GoEntity(Item_sid);
                    Building(temp);
                }
                mybag.Sub(Item_sid);
                Canvas_con();
            }
        }
    }
    public void Building(GameObject build)
    {
        GameObject model = GameObject.Find("System").transform.FindChild("MyDb").GetComponent<MyDb>().BuildingPrefab;
        GameObject buildpos = Instantiate(model, transform.position + transform.forward * 5 + transform.up * 2, Quaternion.identity, transform) as GameObject;
        buildpos.name = "buildpos";
        int buildId = this.transform.parent.FindChild("get_sName").GetComponent<get_sName>().BuildId(build.GetComponent<Self_class>().s_id);
        buildpos.GetComponent<Building>().SetBuildId(buildId);
    }
    public void Equi(int Item_sid)
    {
        GameObject I_temp = mybag.GoEntity(Item_sid);
        I_temp.transform.parent = this.transform.FindChild("Equi");
        string epos = Item.EquiPos(Item_sid);
        if (transform.FindChild("Equi").FindChild(epos) != null)
        {
            GameObject tempequi = transform.FindChild("Equi").FindChild(epos).gameObject;
            GameObject.Find("Environment").transform.FindChild("Item_class").GetComponent<Item_create>().ItemAloneCreate(transform.position, tempequi.GetComponent<Self_class>().s_id);
            UnEqui(tempequi);
            MyCanvas.IssueDis("已装备" + Item.Name(Item_sid) + "之前装备已替下");
            I_temp.name = epos;
        }
        else
        {
            I_temp.transform.parent = transform.FindChild("Equi");
            I_temp.name = epos;
            MyCanvas.IssueDis("已装备" + Item.Name(Item_sid));
        }
        SelfClass.AddAtribute(I_temp, this.gameObject);
        if (epos == "Hand")
        {
            transform.GetComponent<player>().InsWeapon(Item.WeponModeIndex(Item_sid));
        }
    }
    public void UnEqui(GameObject equi)
    {
        SelfClass.SubAtribute(equi, this.gameObject);
        this.gameObject.GetComponent<UnitSyncCmd>().CmdDes(equi);
    }
    public void consume(int Item_id,int c_count)
    {
        mybag.Sub(Item_id, c_count);
    }
}
