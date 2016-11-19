using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class Pocket : MonoBehaviour,IPointerClickHandler {

    // Use this for initialization
    public Sprite Empty_image;
    //private int[] U_Item_id=new int[20];
    //private int[] U_Item_count=new int[20];
    private Transform cplayer;
    private bool isUpdate;
    private bool isOpen;
    private Transform Bag;
    void Awake()
    {
        Bag = this.transform.parent.Find("Pocket").FindChild("PocketBag");
    }
	void Start() {
        cplayer = GameObject.Find("Hero").transform.FindChild("player");
        for (int i = 1; i < 10; i++)
        {
            Bag.FindChild("tP_" + i.ToString()).gameObject.SetActive(false);
        }
        this.transform.parent.Find("Pocket").gameObject.SetActive(false);
        this.transform.parent.Find("Pocket").FindChild("Pan_shortcut").gameObject.SetActive(false);
        isOpen = false;
        isUpdate = false;
	}
    private void Update_canvas(Transform[] U_Item)
    {
        if (isUpdate)                               
        {
            for (int j = 0; j < U_Item.Length; j++)
            {
                if (U_Item[j]==null)
                {
                    Bag.FindChild("P_" + (j + 1).ToString()).GetComponent<Image>().sprite = Empty_image;
                    Bag.FindChild("tP_" + (j + 1).ToString()).GetComponent<Text>().text = " ";
                    Bag.FindChild("tP_" + (j + 1).ToString()).gameObject.SetActive(false);
                }
                else if (U_Item[j].GetComponent<Self_class>().s_iCount > 0)
                {
                    Bag.FindChild("tP_" + (j+1).ToString()).gameObject.SetActive(true);
                    Bag.FindChild("tP_" + (j+1).ToString()).GetComponent<Text>().text = U_Item[j].GetComponent<Self_class>().s_iCount.ToString();
                    Bag.FindChild("P_" + (j + 1).ToString()).GetComponent<Image>().sprite = this.transform.parent.FindChild("Sprite").GetComponent<ImageSprite>().GetSprite(U_Item[j].GetComponent<Self_class>().s_id);
                }
            }
            try
            {

                if (this.transform.parent.FindChild("Pocket").FindChild("MakerSpace").GetComponent<MakerSpace>().status == "Makea")
                {
                    this.transform.parent.FindChild("Pocket").FindChild("MakerSpace").GetComponent<MakerSpace>().Make_a();
                }
            }
            catch { }
            isUpdate = false;
        }
    }
    public void Update_canvas_con(Transform[] Item)
    {
        isUpdate = true;
        if (isOpen)
        {
            //this.transform.parent.Find("Pocket").gameObject.SetActive(true);
            this.Update_canvas(Item);
        }

    }
    public bool Empty_check(int addr)
    {
        return cplayer.GetComponent<player_pocket>().tItem_temp[addr] !=null ? false : true;
    }
    public Transform Item_get(int addr)
    {
        return cplayer.GetComponent<player_pocket>().tItem_temp[addr];
    }
    public void OnPointerClick(PointerEventData eventData)
    {   
        
        isOpen = (isOpen == false ? true : false);
        if (isOpen)
        {                                                                   //更新背包，背包关闭时获得物品
            this.transform.parent.Find("Pocket").gameObject.SetActive(true);
            this.transform.parent.FindChild("Pocket").FindChild("MakerSpace").GetComponent<MakerSpace>().ReStart();
            this.transform.parent.FindChild("Pocket").FindChild("MakerSpace").gameObject.SetActive(true);
            this.transform.parent.FindChild("Pocket").FindChild("Pocket_dashboard").gameObject.SetActive(false);
            this.Update_canvas(cplayer.GetComponent<player_pocket>().tItem_temp);
        }
        else
        {
            this.transform.parent.FindChild("Pocket").FindChild("MakerSpace").GetComponent<MakerSpace>().ReStart();
            this.transform.parent.Find("Pocket").gameObject.SetActive(false);
            this.transform.parent.FindChild("Pocket").FindChild("Pan_shortcut").gameObject.SetActive(false);
        }
    }
}
