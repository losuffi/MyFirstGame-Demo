using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Pan_shortcut : MonoBehaviour,IPointerClickHandler
{
    public Transform cplayer;
    public bool isSet;
    void Start()
    {
    }
    public void OnPointerClick(PointerEventData eventdata)
    {
        string thisName = this.transform.name.Substring(2);
        int TalentId = this.transform.parent.GetComponent<Panshort>().Talent_id;
        if (TalentId == 0)
        {
            int ItemAddr = this.transform.parent.GetComponent<Panshort>().Item_addr;
            isSet = cplayer.GetComponent<play_joystick>().query(thisName);
            cplayer.GetComponent<play_joystick>().set(thisName, ItemAddr - 1);
            string sign = "P_" + thisName + "_s";
            string content = "物品栏" + ItemAddr + cplayer.GetComponent<player_pocket>().tItem_temp[ItemAddr - 1].GetComponent<Self_class>().s_name;
            this.transform.parent.FindChild(sign).GetComponent<Pan_short_signal>().isSet = true;
            this.transform.parent.FindChild(sign).GetComponent<Pan_short_signal>().change();
            this.transform.parent.FindChild(sign).GetComponent<Pan_short_signal>().content = content;
        }
        else
        {
            isSet = cplayer.GetComponent<play_joystick>().query(thisName);
            cplayer.GetComponent<play_joystick>().set(thisName, TalentId);
            string sign = "P_" + thisName + "_s";
            string content = cplayer.parent.FindChild("get_sName").GetComponent<get_sName>().Talent_name(TalentId);
            this.transform.parent.FindChild(sign).GetComponent<Pan_short_signal>().isSet = true;
            this.transform.parent.FindChild(sign).GetComponent<Pan_short_signal>().change();
            this.transform.parent.FindChild(sign).GetComponent<Pan_short_signal>().content = content;
        }
        StartCoroutine(DelayClose());
    }
    IEnumerator DelayClose()
    {
        yield return new WaitForSeconds(0.4f);
        this.transform.parent.gameObject.SetActive(false);
    }
}
