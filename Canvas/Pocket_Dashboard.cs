using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Pocket_Dashboard : MonoBehaviour
{
    private string dash_Item;
    public  int dash_addr;
    private int dash_talentId;
    private string dash_content;
    public  Transform canvas_player;
    private Transform dashUse,dashDrop,dashShortcut,dashStudy;
	void Awake() {
        dash_addr = 1;
        dashUse = this.transform.FindChild("Dash_use");
        dashDrop = this.transform.FindChild("Dash_drop");
        dashShortcut= this.transform.FindChild("Dash_shortcut");
        dashStudy = this.transform.FindChild("Dash_study");
    }
    string  LineFeed(int max_char,string str)
    {
        for (int j = 2; j <= str.Length / max_char; j++)
        {
            str.Insert(max_char * j, "\n");
        }
        return str;
    }
    public void work(int addr,Transform Item)
    {
        dashStudy.gameObject.SetActive(false);
        if(Item.GetComponent<Self_class>().s_iType== "Material")
        {
            dashUse.gameObject.SetActive(false);
            dashShortcut.gameObject.SetActive(false);
        }
        else
        {
            dashDrop.gameObject.SetActive(true);
            dashUse.gameObject.SetActive(true);
            dashShortcut.gameObject.SetActive(true);
        }
        this.transform.parent.FindChild("MakerSpace").gameObject.SetActive(false);
        dash_talentId = 0;
        dash_addr = addr+1;
        dash_Item = Item.GetComponent<Self_class>().s_name;
        dash_content = Item.GetComponent<Self_class>().s_Icontent;
        string Issuetext= "物品栏" + dash_addr.ToString() + " :" + dash_Item; 
        this.transform.FindChild("Issue").GetComponent<Text>().text = Issuetext;
        this.transform.FindChild("Content").GetComponent<Text>().text =dash_content;
    }
    public void workShow(int Iid)
    {
        dashStudy.gameObject.SetActive(false);
        dashUse.gameObject.SetActive(false);
        dashShortcut.gameObject.SetActive(false);
        dashDrop.gameObject.SetActive(false);
        this.transform.parent.FindChild("MakerSpace").gameObject.SetActive(false);
        Transform GetSName = canvas_player.parent.FindChild("get_sName");
        dash_Item = GetSName.GetComponent<get_sName>().Item_name(Iid);
        dash_content = GetSName.GetComponent<get_sName>().Item_content(Iid);
        string Issuetext = "物品名：" + dash_Item;
        this.transform.FindChild("Issue").GetComponent<Text>().text = Issuetext;
        this.transform.FindChild("Content").GetComponent<Text>().text = dash_content;
    }
    public void Talent_dash(Transform Talent)
    {
        dashStudy.gameObject.SetActive(false);
        dashDrop.gameObject.SetActive(false);
        dashUse.gameObject.SetActive(false);
        if (Talent.GetComponent<Self_class>().s_iType == "active")
        {
            dashShortcut.gameObject.SetActive(true);
        }
        else
        {
            dashShortcut.gameObject.SetActive(false);
        }
        this.transform.parent.FindChild("MakerSpace").gameObject.SetActive(false);
        dash_talentId = Talent.GetComponent<Self_class>().s_id;
        dash_Item = Talent.GetComponent<Self_class>().s_name;
        dash_content = Talent.GetComponent<Self_class>().s_Icontent;
        string Issuetext = "天赋名：" + dash_Item;
        this.transform.FindChild("Issue").GetComponent<Text>().text = Issuetext;
        this.transform.FindChild("Content").GetComponent<Text>().text = dash_content;
    }
    public void TalentStudy_dash(int Tid)
    {
        dashDrop.gameObject.SetActive(false);
        dashUse.gameObject.SetActive(false);
        dashShortcut.gameObject.SetActive(false);
        if (canvas_player.GetComponent<Self_class>().TalentFreePoint > 0)
        {
            dashStudy.gameObject.SetActive(true);
        }
        else
        {
            dashStudy.gameObject.SetActive(false);
        }
        this.transform.parent.FindChild("MakerSpace").gameObject.SetActive(false);
        Transform GetSName= canvas_player.parent.FindChild("get_sName");
        dash_talentId = Tid;
        dash_Item = GetSName.GetComponent<get_sName>().Talent_name(Tid);
        dash_content = GetSName.GetComponent<get_sName>().Talent_Content(Tid);
        string Issuetext = "天赋名：" + dash_Item;
        this.transform.FindChild("Issue").GetComponent<Text>().text = Issuetext;
        this.transform.FindChild("Content").GetComponent<Text>().text = dash_content;
    }
    public void drop()
    {
        canvas_player.GetComponent<player_pocket>().drop(dash_addr-1);
        string res = "Start";
        while (res!= "Empty"){
            res = canvas_player.GetComponent<play_joystick>().query_i((dash_addr - 1));
            if (res == "Empty")
            {
                break;
            }
            else
            {
                this.transform.parent.FindChild("Pan_shortcut").GetComponent<Panshort>().Drop_es(res);
                canvas_player.GetComponent<play_joystick>().remove(res);
            }
        }
        this.close();
    }
    public void close()
    {
        this.transform.parent.FindChild("MakerSpace").gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void shortcut()
    {
        this.gameObject.SetActive(false);
        this.transform.parent.FindChild("Pan_shortcut").gameObject.SetActive(true);
        if (dash_talentId == 0)
        {
            this.transform.parent.FindChild("Pan_shortcut").GetComponent<Panshort>().Item_addr = dash_addr;
        }
        else
        {
            this.transform.parent.FindChild("Pan_shortcut").GetComponent<Panshort>().Talent_id = dash_talentId;
        }
    }
    public void use()
    {
        canvas_player.GetComponent<player_pocket>().use(dash_addr - 1);
        //canvas_player.parent.FindChild("Spell").GetComponent<Spell_cast>().casting(this.transform.FindChild("get_sName").GetComponent<get_sName>().Item_Spell(Item_id), canvas_player);
    }
    public void study()
    {
        this.transform.parent.FindChild("MakerSpace").GetComponent<MakerSpace>().ReStart();
        this.close();
        canvas_player.GetComponent<PlayerTalent>().Study(dash_talentId);
    }
}
