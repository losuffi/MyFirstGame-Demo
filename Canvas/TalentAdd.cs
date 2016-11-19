using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class TalentAdd : MonoBehaviour,IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventdata)
    {
        Transform TalentBoard = this.transform.parent.parent.FindChild("TalentBoard");
        TalentBoard.GetComponent<TalentBoard>().add_Board();
        if (this.transform.parent.GetComponent<MakerSpace>().cplayer.GetComponent<Self_class>().TalentFreePoint > 0)
        {
            TalentBoard.GetComponent<TalentBoard>().WorkABoard();
        }
    }
}
