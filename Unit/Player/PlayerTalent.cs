using UnityEngine;
using System.Collections;

public class PlayerTalent : MonoBehaviour {
    public Hashtable TalentHash=new Hashtable();
    private int Addr;
    public Transform TalentModel;
    void Awake()
    {
        Addr = 0;
    }
    public void GetTalentStudy()
    {
        this.transform.GetComponent<Self_class>().TalentFreePoint++;
    }
    public bool Study(int Tid)
    {
        if (TalentHash.Contains(Tid))
        {
            return false;
        }else
        {
            this.transform.parent.FindChild("Talent").GetComponent<TalentCenter>().Work(Tid, this.transform);
            this.transform.GetComponent<Self_class>().TalentFreePoint--;
            TalentHash.Add(Tid, Addr);
            Transform TalentTemp = (Transform)Instantiate(TalentModel, this.transform);
            TalentTemp.name = "Talent_" + Addr;
            ConBoard();
            Addr++;
            return true;
        }
    }
    private void ConBoard()
    {
        this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("Pocket").FindChild("TalentBoard").GetComponent<TalentBoard>().WorkBoard();
    }
    public void TalentCast(int Tid)
    {
        string  spell_Talent = this.transform.GetComponent<Self_class>().s_name+"使用了"+this.transform.parent.FindChild("get_sName").GetComponent<get_sName>().Talent_name(Tid);
        int spell_id = this.transform.parent.FindChild("get_sName").GetComponent<get_sName>().Talent_SpellId(Tid);
        this.transform.GetComponent<Self_class>().s_Canvas.transform.FindChild("IssueBoard").GetComponent<IssueBoard>().dis(spell_Talent);
        this.transform.parent.FindChild("Spell").GetComponent<Spell_cast>().casting(spell_id, this.transform);
    }
}
