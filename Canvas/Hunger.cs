using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Hunger : MonoBehaviour {
    public Transform cplayer;
    private Vector2 Initsize;
    private float Ratio;
    private Transform mText;
    private bool isInit = false;
    void Awake()
    {
        mText = this.transform.parent.FindChild("Text");
        Initsize = this.GetComponent<RectTransform>().rect.size;
        Ratio = 1;
    }
    void FixedUpdate()
    {
        if (isInit)
        {
            Ratio = cplayer.GetComponent<Self_class>().getHunger() / cplayer.GetComponent<Self_class>().s_Hunger;
            this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, Initsize.y * Ratio);
            mText.GetComponent<Text>().text = cplayer.GetComponent<Self_class>().getHunger() + "/" + cplayer.GetComponent<Self_class>().s_Hunger;
        }
    }
    public void Init()
    {
        cplayer = this.transform.parent.parent.GetComponent<Canvas_Init>().cplayer;
        isInit = true;
    }
}
