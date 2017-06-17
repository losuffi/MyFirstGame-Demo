using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class NUIClick : MonoBehaviour,IPointerClickHandler {
    private string Tname;
    public void OnPointerClick(PointerEventData eventdata)
    {
        NUIInit.NI.Click(transform.name);
    }
}
