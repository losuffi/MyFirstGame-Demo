using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class Pocket_current :MonoBehaviour,IPointerClickHandler
{
    private bool isEmpty;
    private int addr;
    private Transform Item;
    private Transform BPoc;
    private Transform Dashb;
	// Use this for initialization
    void Awake()
    {
        BPoc = this.transform.parent.parent.parent.FindChild("B_poc");
        Dashb = this.transform.parent.parent.FindChild("Pocket_dashboard");
    }
    int getaddr(string str)
    {
        int addr = str[str.Length - 1] - 49;
        return addr;
    }
	void Start() {
        isEmpty = true;
        addr = getaddr(this.gameObject.name);
	}
	
	// Update is called once per frame
	public void OnPointerClick(PointerEventData eventData)
    {
        isEmpty = BPoc.GetComponent<Pocket>().Empty_check(addr);
        Item = BPoc.GetComponent<Pocket>().Item_get(addr);
        if (!isEmpty)
        {

            Dashb.gameObject.SetActive(true);
            Dashb.GetComponent<Pocket_Dashboard>().work(addr,Item);
        }
    }
}
