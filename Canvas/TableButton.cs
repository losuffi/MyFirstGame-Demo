using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class TableButton : MonoBehaviour,IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventdata)
    {
        if (transform.name == "Person")
        {
            Person();
        }
        else if (transform.name == "Bag")
        {
            Bag();
        }
        else if(transform.name == "Compose")
        {
            Compose();
        }
    }
    void Person()
    {
        GameObject.Find("Canvas").transform.FindChild("Center").GetComponent<CanvasCenter>().OpenPerson();
    }
    void Bag()
    {
        GameObject.Find("Canvas").transform.FindChild("Center").GetComponent<CanvasCenter>().OpenBag();
    }
    void Compose()
    {
        GameObject.Find("Canvas").transform.FindChild("Center").GetComponent<CanvasCenter>().OpenMaker();
    }
}
