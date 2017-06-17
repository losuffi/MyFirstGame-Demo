using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class IssueBoard : MonoBehaviour{
    private Transform Board;
    private float HeightInit,Height;
    private int MsgCount;
    void Awake()
    {
        Board = this.transform.FindChild("Board");
        MsgCount = 0;
        HeightInit = Board.GetComponent<RectTransform>().rect.size.y;
    }
    public void dis(string str)
    {
        MsgCount++;
        if (MsgCount > 10)
        {
            MsgCount = 1;
            Board.GetComponent<Text>().text = "";
            Board.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, HeightInit);
        }
        if (MsgCount > 1)
        {
            str = "\n<Msg>" + str;
        }
        else
        {
            str="<Msg>" + str;
        }
        int y = 1 + str.Length / 21;
        Board.GetComponent<Text>().text +=str;
        Height= Board.GetComponent<RectTransform>().rect.size.y;
        if (MsgCount > 3)
        {
            Board.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, Height + y * 15.0f);
        }
    }
}
