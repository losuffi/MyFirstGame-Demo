using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
public class BackMenu : NetworkBehaviour,IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventdata)
    {
        GameObject.Find("Hero").transform.FindChild("Player").FindChild("PlayerCamera").SetParent(null);
        NetworkManager.Shutdown();
        SceneManager.LoadScene(0);
    }
}
