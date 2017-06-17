using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Canvas_Init : MonoBehaviour {
    public Transform cplayer;
    public void BindPlayer(Transform pl)
    {
        cplayer = pl;
        Init();
    }
    public void Init()
    {
        this.transform.FindChild("Center").GetComponent<Pocket>().Init();
        this.transform.FindChild("Center").GetComponent<CanvasCenter>().Init(cplayer);
        this.transform.FindChild("S_Life").GetComponent<Scroll_life>().Init();
        this.transform.FindChild("Hunger").FindChild("HungerScroll").GetComponent<Hunger>().Init();
        this.transform.FindChild("Gameover").GetComponent<GameOver>().cplayer = cplayer;
    }
}
