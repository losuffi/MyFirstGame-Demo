using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Scroll_life : MonoBehaviour {
    public Transform cplayer;
    private float plife;
    private float slife;
    private bool isInit = false;
	void FixedUpdate () {
        if (isInit)
        {
            Work();
        }
	}
    void Work()
    {
        plife = cplayer.GetComponent<Self_class>().getLife();
        slife = cplayer.GetComponent<Self_class>().s_life;
        this.gameObject.GetComponent<Scrollbar>().size = plife / slife;
        this.gameObject.GetComponent<Scrollbar>().value = 0;
        this.transform.Find("T_pl").GetComponent<Text>().text = ((int)plife).ToString() + "/" + ((int)slife).ToString();
    }
    public void Init()
    {
        cplayer = this.transform.parent.GetComponent<Canvas_Init>().cplayer;
        isInit = true;
    }
}
