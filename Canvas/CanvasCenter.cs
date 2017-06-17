using UnityEngine;
using System.Collections;

public class CanvasCenter : MonoBehaviour {
    private bool PersonIsOpen,MakerIsopen;
    private Transform MsgBoard,MakerSpace;
    void Awake()
    {
        MsgBoard = this.transform.parent.Find("PersonMsgBoard");
        MakerSpace = this.transform.parent.Find("MakerSpace");
    }
    public void Init(Transform cplayer)
    {
        this.transform.parent.FindChild("PersonMsgBoard").GetComponent<PersonMsgBoard>().cplayer = cplayer;
        this.transform.parent.FindChild("TalentBoard").GetComponent<TalentBoard>().cplayer = cplayer;
        this.transform.parent.FindChild("TalentBoard").GetComponent<TalentBoard>().Init();
        this.transform.parent.FindChild("TalentBoard").GetComponent<TalentBoard>().GetsName = cplayer.parent.FindChild("get_sName");
        this.transform.parent.FindChild("MakerSpace").GetComponent<MakerSpace>().Init();
        this.transform.parent.FindChild("PersonMsgBoard").gameObject.SetActive(false);
        this.transform.parent.FindChild("TalentBoard").gameObject.SetActive(false);
        this.transform.parent.FindChild("PocketBag").gameObject.SetActive(false);
    }
    public void OpenPerson()
    {
        PersonIsOpen = PersonIsOpen ? false : true;
        MsgBoard.gameObject.SetActive(PersonIsOpen);
        if (PersonIsOpen)
        {
            MsgBoard.GetComponent<PersonMsgBoard>().Work();
        }
    }
    public void OpenMaker()
    {
        MakerIsopen = MakerIsopen ? false : true;
        MakerSpace.gameObject.SetActive(MakerIsopen);
        if (MakerIsopen)
        {
            MakerSpace.GetComponent<MakerSpace>().Work();
        }
    }
    public void OpenBag()
    {
        this.GetComponent<Pocket>().OpenBag();
    }
}
