using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TalentBoard : MonoBehaviour{
    public Transform cplayer;
    private bool isBoardUpdate,isABoardUpdate;
    private Transform SpriteImage;
    private Transform Board,ABoard;
    public Transform TalentMode_List;
    public Transform GetsName;
    void Awake()
    {
        Board = this.transform.FindChild("List");
        ABoard = this.transform.FindChild("List_1");
        SpriteImage = this.transform.FindChild("TalentSprite");
    }
    void Start()
    {
        GetsName = cplayer.parent.FindChild("get_sName");
        isBoardUpdate = false;
        isABoardUpdate = true;
    }
    public void WorkBoard()
    {
        isBoardUpdate = true;
    }
    public void WorkABoard()
    {
        isABoardUpdate = true;
    }
    public void m_Board()
    {
        ABoard.gameObject.SetActive(false);
        Board.gameObject.SetActive(true);
        if (isBoardUpdate)
        {
            for(int i=0;i<Board.childCount;i++)
            {
                Destroy(Board.GetChild(i).gameObject);
            }
            foreach(DictionaryEntry de in cplayer.GetComponent<PlayerTalent>().TalentHash)
            {
                int tid = (int)de.Key;
                Transform TalentTemp =(Transform)Instantiate(TalentMode_List, Board);
                TalentTemp.GetComponent<Image>().sprite = SpriteImage.GetComponent<ImageSprite>().GetTalentSprite(tid); //tid
                TalentTemp.name = "Talent_" + (int)de.Value;
                TalentTemp.GetComponent<TalentCurrent>().InitCurrent(tid,(int)de.Value,cplayer);
            }
            isBoardUpdate = false;
        }
    }
    public void add_Board()
    {
        ABoard.gameObject.SetActive(true);
        Board.gameObject.SetActive(false);
        int Count = GetsName.GetComponent<get_sName>().Talent_Count();
        if (isABoardUpdate)
        {
            for (int i = 0; i < ABoard.childCount; i++)
            {
                Destroy(ABoard.GetChild(i).gameObject);
            }
            for(int j = 0; j < Count; j++)
            {
                int ctid = j + 50001;
                int ptid = GetsName.GetComponent<get_sName>().Talent_parent(ctid);
                if (IsHaveParentTalent(ptid, ctid))
                {
                    Transform TalentTemp = (Transform)Instantiate(TalentMode_List, ABoard);
                    TalentTemp.GetComponent<Image>().sprite = SpriteImage.GetComponent<ImageSprite>().GetTalentSprite(ctid); //tid
                    TalentTemp.name = "Talent_"+(ctid);
                    TalentTemp.GetComponent<TalentCurrent>().InitACurrent(ctid);
                }
            }
            isABoardUpdate = false;
        }
    }
    private bool IsHaveParentTalent(int ptid,int ctid)
    {
        bool isHaveP,isHaveC;
        isHaveP = false;
        isHaveC = false;
        foreach (DictionaryEntry de in cplayer.GetComponent<PlayerTalent>().TalentHash)
        {
            int tid = (int)de.Key;
            if (ptid == tid)
            {
                isHaveP = true;
            }
            if (ctid == tid)
            {
                isHaveC = true;
            }
        }
        if (ptid == 50000)
        {
            isHaveP = true;
        }
        return (isHaveP && !isHaveC);
    }
}
