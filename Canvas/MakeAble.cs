using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MakeAble : MonoBehaviour, IPointerClickHandler{
    
    public void OnPointerClick(PointerEventData eventdata)
    {
        if (this.transform.name == "MakerAble") {
            this.transform.parent.GetComponent<MakerSpace>().Make_a();
        }
        else if(this.transform.name == "MakerUnable")
        {
            this.transform.parent.GetComponent<MakerSpace>().Make_all();
        }
    }
}
