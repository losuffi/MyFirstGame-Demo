using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MakerSpace : MonoBehaviour {
    public Transform cplayer;
    public string status;
    public void Start()
    {
        ReStart();
        this.transform.GetComponent<ComposeTable>().Com_Init();
    }
    public void Work_Composite()
    {
        ReStart();
        status = "Work";
        this.transform.FindChild("MakerAble").gameObject.SetActive(true);
        this.transform.FindChild("MakerUnable").gameObject.SetActive(true);
        this.transform.FindChild("Maker").gameObject.SetActive(false);
        this.transform.FindChild("Pre").gameObject.SetActive(true);
    }
    public void Work_Bag()
    {
        ReStart();
        this.transform.FindChild("Pre").gameObject.SetActive(true);
        this.transform.parent.FindChild("PocketBag").gameObject.SetActive(true);
    }
    public void Work_Talent()
    {
        ReStart();
        status = "Work";
        this.transform.parent.FindChild("TalentBoard").gameObject.SetActive(true);
        this.transform.parent.FindChild("TalentBoard").GetComponent<TalentBoard>().m_Board();
        this.transform.FindChild("Pre").gameObject.SetActive(true);
        this.transform.FindChild("TalentAdd").gameObject.SetActive(true);
        this.transform.FindChild("TalentAdd").FindChild("Issue").GetComponent<Text>().text = "空闲天赋点：" + cplayer.GetComponent<Self_class>().TalentFreePoint;
        this.transform.FindChild("Maker").gameObject.SetActive(false);
    }
    public void Work_PersonMsg()
    {
        ReStart();
        status = "Work";
        this.transform.parent.FindChild("PersonMsgBoard").gameObject.SetActive(true);
        this.transform.parent.FindChild("PersonMsgBoard").GetComponent<PersonMsgBoard>().Work();
    }
    public void Make_a()
    {
        ReStart();
        status = "Makea";
        this.transform.FindChild("Maker").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_List").gameObject.SetActive(true);
        this.transform.FindChild("Scroll_AllList").gameObject.SetActive(false);
        this.transform.FindChild("Pre").gameObject.SetActive(true);
        this.transform.FindChild("Scroll_List").FindChild("MakeList").GetComponent<MakeList>().disp();
    }
    public void Make_all()
    {
        ReStart();
        status = "Makeall";
        this.transform.FindChild("Maker").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_AllList").gameObject.SetActive(true);
        this.transform.FindChild("Scroll_List").gameObject.SetActive(false);
        this.transform.FindChild("Pre").gameObject.SetActive(true);
        this.transform.FindChild("Scroll_AllList").FindChild("MakeAllList").GetComponent<MakeList>().dispAll();
    }
    public void ReStart()
    {
        status = "Start";
        this.transform.FindChild("Maker").gameObject.SetActive(true);
        this.transform.FindChild("Pre").gameObject.SetActive(false);
        this.transform.FindChild("MakerAble").gameObject.SetActive(false);
        this.transform.FindChild("MakerUnable").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_List").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_AllList").gameObject.SetActive(false);
        this.transform.FindChild("TalentAdd").gameObject.SetActive(false);
        this.transform.parent.FindChild("PersonMsgBoard").gameObject.SetActive(false);
        this.transform.parent.FindChild("TalentBoard").gameObject.SetActive(false);
        this.transform.parent.FindChild("PocketBag").gameObject.SetActive(false);
    }
}
