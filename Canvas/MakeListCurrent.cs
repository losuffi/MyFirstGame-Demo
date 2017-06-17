using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MakeListCurrent : MonoBehaviour,IPointerClickHandler {
    public int Item_id;
    public string compose;
    public int Item_count=1;
    public Transform cplayer;
    public int type;
    public void OnPointerClick(PointerEventData eventdata) 
    {
        if (type == 1)
        {
            transform.parent.GetComponent<MakeList>().disp();
            cplayer.GetComponent<PlayerMake>().Work(Item_id, compose, Item_count);
        }
        else
        {
            Transform Dashb = this.transform.parent.parent.parent.parent.FindChild("Pocket_dashboard");
            Dashb.GetComponent<Pocket_Dashboard>().workShow(Item_id);
            Dashb.gameObject.SetActive(true);
        }
    }
}
