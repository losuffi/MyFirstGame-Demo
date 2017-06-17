using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BaseNUI : MonoBehaviour, IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventdata)
    {
        if (this.transform.name == "CreateGame")
        {
            GameObject.Find("System").transform.FindChild("GameManager").GetComponent<GameManager>().CreateGame();
        }
        else if (this.transform.name == "JoinGame")
        {
            string adre = this.transform.FindChild("IP").FindChild("Adress").GetComponent<Text>().text;
            GameObject.Find("System").transform.FindChild("GameManager").GetComponent<GameManager>().JoinGame(adre);
        }
    }
}
