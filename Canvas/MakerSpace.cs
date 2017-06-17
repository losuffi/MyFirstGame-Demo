using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using _spellLib;
public class MakerSpace : MonoBehaviour {
    public Transform cplayer;
    public string status;
    public void Init()
    {
        cplayer = this.transform.parent.GetComponent<Canvas_Init>().cplayer;
        this.transform.GetComponent<ComposeTable>().Com_Init();
        this.transform.GetComponent<ComposeTable>().cplayer = cplayer;
        ReStart();
    }
    public void Make_a()
    {
        status = "Makea";
        this.transform.FindChild("Scroll_List").gameObject.SetActive(true);
        this.transform.FindChild("Scroll_AllList").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_ListTable").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_List").FindChild("MakeList").GetComponent<MakeList>().disp();
    }
    public void Make_all()
    {
        status = "Makeall";
        this.transform.FindChild("Scroll_AllList").gameObject.SetActive(true);
        this.transform.FindChild("Scroll_List").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_ListTable").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_AllList").FindChild("MakeAllList").GetComponent<MakeList>().dispAll();
    }
    public void Make_table()
    {
        if (StaticScan())
        {
            status = "Maketable";
            this.transform.FindChild("Scroll_ListTable").gameObject.SetActive(true);
            this.transform.FindChild("Scroll_List").gameObject.SetActive(false);
            this.transform.FindChild("Scroll_AllList").gameObject.SetActive(false);
            this.transform.FindChild("Scroll_ListTable").FindChild("MakeListTable").GetComponent<MakeList>().disptable();
            StartCoroutine(scan());
        }
        else
        {
            MyCanvas.IssueDis("人物面前没有制作所需的建筑");
            ReStart();
        }
    }
    public void ReStart()
    {
        status = "Start";
        this.transform.FindChild("MakerAble").gameObject.SetActive(false);
        this.transform.FindChild("MakerUnable").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_List").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_AllList").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_ListTable").gameObject.SetActive(false);
        this.transform.FindChild("TalentAdd").gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
    }
    public void Work()
    {
        status = "Start";
        this.transform.FindChild("MakerAble").gameObject.SetActive(true);
        this.transform.FindChild("MakerUnable").gameObject.SetActive(true);
        this.transform.FindChild("Scroll_List").gameObject.SetActive(false);
        this.transform.FindChild("Scroll_AllList").gameObject.SetActive(false);
    }
    IEnumerator scan()
    {
        while (true)
        {
            RaycastHit hit;
            bool Tempflag = false;
            for (int i = 0; i < 3; i++) {
                if (Physics.Raycast(cplayer.transform.position+(i*0.4f*cplayer.up),cplayer.transform.forward,out hit, 2))
                {
                    if(hit.transform.name== "WorkTable")
                    {
                        Tempflag = true;
                        break;
                    }
                }
            }
            if (!Tempflag)
            {
                MyCanvas.IssueDis("人物已离开制作所需的建筑");
                ReStart();
                StopCoroutine(scan());
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    bool StaticScan()
    {
        RaycastHit hit;
        bool Tempflag = false;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(cplayer.transform.position + (i * 0.4f * cplayer.up), cplayer.transform.forward, out hit, 2))
            {
                if (hit.transform.name == "WorkTable")
                {
                    Tempflag = true;
                    break;
                }
            }
        }
        return Tempflag;
    }
}
