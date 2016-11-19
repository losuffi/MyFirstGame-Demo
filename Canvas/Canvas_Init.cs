using UnityEngine;
using System.Collections;

public class Canvas_Init : MonoBehaviour {
    public Transform cplayer;
    void Awake()
    {
        this.transform.FindChild("Bars").GetComponent<Bars_i>().cplayer = cplayer;
        this.transform.FindChild("PlayDirect").GetComponent<PlayDirect>().cplayer = cplayer;
        this.transform.FindChild("Image").FindChild("B_B").GetComponent<Joystick>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("Pan_shortcut").FindChild("P_Up").GetComponent<Pan_shortcut>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("Pan_shortcut").FindChild("P_Down").GetComponent<Pan_shortcut>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("Pan_shortcut").FindChild("P_Left").GetComponent<Pan_shortcut>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("Pan_shortcut").FindChild("P_Right").GetComponent<Pan_shortcut>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("MakerSpace").GetComponent<MakerSpace>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("MakerSpace").GetComponent<ComposeTable>().cplayer = cplayer;
        this.transform.FindChild("Hunger").FindChild("HungerScroll").GetComponent<Hunger>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("TalentBoard").GetComponent<TalentBoard>().cplayer = cplayer;
        this.transform.FindChild("Pocket").FindChild("Pocket_dashboard").GetComponent<Pocket_Dashboard>().canvas_player = cplayer;
        this.transform.FindChild("Pocket").FindChild("PersonMsgBoard").GetComponent<PersonMsgBoard>().cplayer = cplayer;
        this.transform.FindChild("Gameover").GetComponent<GameOver>().cplayer = cplayer;
    }
}
