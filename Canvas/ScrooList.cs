using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class ScrooList : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    private Vector3 poszero;
    private Vector3 pos;
    public int ablecount;
    void Start()
    {
        if (this.transform.name == "Scroll_List")
        {
            poszero = this.transform.FindChild("MakeList").position;
        }else
        {
            poszero = this.transform.FindChild("MakeAllList").position;
        }
    }
    public void OnPointerDown(PointerEventData eventdata)
    {
        if (this.transform.name == "Scroll_List")
        {
            ablecount = this.transform.FindChild("MakeList").GetComponent<MakeList>().AbleCount;
            pos = this.transform.FindChild("MakeList").position;
        }
        else
        {
            ablecount = this.transform.FindChild("MakeAllList").GetComponent<MakeList>().AbleCount;
            pos = this.transform.FindChild("MakeAllList").position;
        }
    }
    public void OnPointerUp(PointerEventData eventdata)
    {
        if (this.transform.name == "Scroll_List")
        {
            if (this.transform.FindChild("MakeList").position.y - poszero.y > ablecount * 130 || this.transform.FindChild("MakeList").position.y - poszero.y < -4 * 130)
            {
                this.transform.FindChild("MakeList").position = pos;
            }
        }
        else
        {
            if (this.transform.FindChild("MakeAllList").position.y - poszero.y > ablecount * 130 || this.transform.FindChild("MakeAllList").position.y - poszero.y < -4 * 130)
            {
                this.transform.FindChild("MakeAllList").position = pos;
            }
        }
    }
}
