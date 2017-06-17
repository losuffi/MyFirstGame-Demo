using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using _spellLib;
public class Pan_shortcut : MonoBehaviour,IPointerClickHandler
{
    private bool IsOpen;
    private KeyCode currentkey;
    public  static ShortCuts _sc;
    public int Id;
    public void Clean()
    {
        currentkey = _sc.GetKey(Id);
        if (currentkey != KeyCode.Joystick1Button19)
        {
            this.transform.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1);
            this.transform.Find("Text").GetComponent<Text>().text = currentkey.ToString();
        }
        else
        {
            this.transform.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1);
            this.transform.Find("Text").GetComponent<Text>().text = " ";
        }
    }
    void Start()
    {
        _sc = new ShortCuts();
    }
    void OnGUI()
    { 
        if (IsOpen)
        {
            this.transform.Find("Text").GetComponent<Text>().color = new Color(1, 1, 0);
            if (Input.anyKeyDown)
            {
                Event fee = Event.current;
                if (fee.isKey)
                {
                    currentkey = fee.keyCode;
                    if (currentkey.ToString() != "None")
                    {
                        this.transform.Find("Text").GetComponent<Text>().color = new Color(1, 1, 1);
                        this.transform.Find("Text").GetComponent<Text>().text = currentkey.ToString();
                        _sc.AddShort(currentkey, Id);
                        IsOpen = false;
                    }
                }
            }
        }
    }
    public void OnPointerClick(PointerEventData eventdata)
    {
        IsOpen = IsOpen ? false : true;
    }
}
