using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class TalentCurrent : MonoBehaviour,IPointerClickHandler{
    private Transform cplayer;
    private  bool isStudy;
    private int mTid;
    public void InitCurrent(int Tid,int Taddr,Transform zplayer)
    {
        isStudy = false;
        cplayer = zplayer;
        Transform GetsName = cplayer.parent.FindChild("get_sName");
        Transform TalentTemp = cplayer.FindChild("Talent_" + Taddr);
        TalentTemp.GetComponent<Self_class>().s_name = GetsName.GetComponent<get_sName>().Talent_name(Tid);
        TalentTemp.GetComponent<Self_class>().s_iType = GetsName.GetComponent<get_sName>().Talent_Type(Tid);
        TalentTemp.GetComponent<Self_class>().s_Icontent = GetsName.GetComponent<get_sName>().Talent_Content(Tid);
    }
    public void InitACurrent(int Tid)
    {
        mTid = Tid;
        isStudy = true;
    }
    public void OnPointerClick(PointerEventData eventdata)
    {
        Transform Dashb = this.transform.parent.parent.parent.FindChild("Pocket_dashboard");
        Dashb.gameObject.SetActive(true);
        if (isStudy)
        {
            Dashb.GetComponent<Pocket_Dashboard>().TalentStudy_dash(mTid);
        }
        else
        {
            string TalentTempName = this.transform.name;
            Transform TalentTemp = cplayer.FindChild(TalentTempName);
            Dashb.GetComponent<Pocket_Dashboard>().Talent_dash(TalentTemp);
        }
    }
}
