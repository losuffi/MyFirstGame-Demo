using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MenuCurrent : MonoBehaviour,IPointerClickHandler {
    private AsyncOperation Asy;
    public void OnPointerClick(PointerEventData eventdata)
    {
        string keyname = this.transform.name;
        Transform cCanvas = GameObject.Find("Canvas").transform;
        if (keyname == "Author")
        {
            cCanvas.GetComponent<Menu>().NuiAu();
        }
        else if (keyname == "Start")
        {
            cCanvas.GetComponent<Menu>().NuiSu();
        }
        else if (keyname == "Exit")
        {
            cCanvas.GetComponent<Menu>().NuiMu();
        }
    }
}
