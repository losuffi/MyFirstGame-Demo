using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using _spellLib;
using GetSName;
using UnityEngine.UI;

public class Pocket : MonoBehaviour{
    public Sprite Empty_image;
    private Transform cplayer,Bag;
    private bool isUpdate,isOpen;
    public void Init()
    {
        cplayer = this.transform.parent.GetComponent<Canvas_Init>().cplayer;
        this.transform.parent.FindChild("Pan_shortcut").GetComponent<Panshort>().cplayer = cplayer;
        this.transform.parent.FindChild("Pocket_dashboard").GetComponent<Pocket_Dashboard>().canvas_player = cplayer;
        this.transform.parent.FindChild("Pan_shortcut").gameObject.SetActive(false);
        this.transform.parent.Find("Pocket_dashboard").gameObject.SetActive(false);
    }
    void Awake()
    {
        Bag = this.transform.parent.Find("PocketBag");
    }
	void Start() {
        for (int i = 1; i < 10; i++)
        {
            Bag.FindChild("tP_" + i.ToString()).gameObject.SetActive(false);
        }
        isOpen = false;
        isUpdate = false;
	}
    void Clean()
    {
        for (int i = 1; i < 10; i++)
        {
            Bag.FindChild("tP_" + i).gameObject.SetActive(false);
            Bag.FindChild("P_" + i).GetComponent<Image>().sprite = Item.Geizi;
            Bag.FindChild("P_" + i).GetComponent<Pocket_current>().Set(0);
        }
    }
    public void Update_canvas()
    {
        if (isUpdate)
        {
            Clean();
            int j = 0;
            ICollection de = cplayer.GetComponent<player_pocket>().mybag.SpaceKeys();
            foreach (int c in de)
            {
                j++;
                Bag.FindChild("tP_" + j).gameObject.SetActive(true);
                Bag.FindChild("tP_" + j).GetComponent<Text>().text = cplayer.GetComponent<player_pocket>().Query(c).ToString();
                Bag.FindChild("P_" + j).GetComponent<Image>().sprite = Item.ISprite(c);
                Bag.FindChild("P_" + j).GetComponent<Pocket_current>().Set(c);
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
    public void Update_canvas_con()
    {
        isUpdate = true;
        if (isOpen)
        {
            Update_canvas();
        }

    }
    public void OpenBag()
    {
        isOpen = isOpen ? false : true;
        Bag.gameObject.SetActive(isOpen);
        if (isOpen)
        {
            Update_canvas();
        }
        else
        {
            transform.parent.Find("Pocket_dashboard").gameObject.SetActive(false);
            transform.parent.Find("Pan_shortcut").gameObject.SetActive(false);
        }
    }
}
