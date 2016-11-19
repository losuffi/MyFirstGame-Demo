using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class BackMenu : MonoBehaviour,IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventdata)
    {
        SceneManager.LoadScene(0);
    }
}
