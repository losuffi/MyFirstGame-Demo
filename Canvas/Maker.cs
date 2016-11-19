using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Maker : MonoBehaviour,IPointerClickHandler{
    private Transform cplayer;
    private Transform Space;
    void Awake()
    {
        Space = this.transform.parent.parent;
    }
    public void OnPointerClick(PointerEventData eventdata)
    {
        if (this.transform.name == "Composite")
        {
            Space.GetComponent<MakerSpace>().Work_Composite();
        }
        else if (this.transform.name == "Talent")
        {
            Space.GetComponent<MakerSpace>().Work_Talent();
        }
        else if (this.transform.name == "Bag")
        {
            Space.GetComponent<MakerSpace>().Work_Bag();
        }
        else if(this.transform.name == "PersonMsg")
        {
            Space.GetComponent<MakerSpace>().Work_PersonMsg();
        }
        else if(this.transform.name == "MakerAble")
        {
            Space.GetComponent<MakerSpace>().Make_a();
        }
        else if(this.transform.name == "MakerUnable")
        {
            Space.GetComponent<MakerSpace>().Make_a();
        }
    }
}
