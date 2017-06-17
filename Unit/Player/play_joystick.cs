using UnityEngine;
using System.Collections;
using _spellLib;
public class play_joystick : MonoBehaviour {
    void OnGUI()
    {
        if (Input.anyKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                Pan_shortcut._sc.KeyTrig(e.keyCode, this.gameObject);
            }
        }
    }
}
