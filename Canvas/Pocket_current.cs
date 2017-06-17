using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Pocket_current :MonoBehaviour,IPointerClickHandler
{
    private Transform Dashb;
    private int ItemId;
	// Use this for initialization
    void Awake()
    {
        Dashb = this.transform.parent.parent.FindChild("Pocket_dashboard");
    }	
	// Update is called once per frame
	public void OnPointerClick(PointerEventData eventData)
    {
        //Item = Center.GetComponent<Pocket>().Item_get(addr);
        if (ItemId!=0)
        {

            Dashb.gameObject.SetActive(true);
            Dashb.GetComponent<Pocket_Dashboard>().work(ItemId);
        }
    }
    public void Set(int i)
    {
        ItemId = i;
    }
}
