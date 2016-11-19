using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MakePre : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventdata)
    {
        this.transform.parent.GetComponent<MakerSpace>().ReStart();
    }
}
