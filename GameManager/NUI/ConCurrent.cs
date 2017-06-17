using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ConCurrent : MonoBehaviour,IPointerClickHandler {
    public delegate void GetIP(string ip,GameObject o);
    public event GetIP get;
    public void OnPointerClick(PointerEventData eventdata)
    {
        if (get != null)
            get(this.name,this.gameObject);
    }
}
