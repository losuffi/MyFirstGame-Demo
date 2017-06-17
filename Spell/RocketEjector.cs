using UnityEngine;
using System.Collections;

public class RocketEjector : MonoBehaviour {
    private GameObject Trig;
    public void Work(Transform Trig_Unit)
    {
        Trig = Trig_Unit.gameObject;
        Trig_Unit.GetComponent<Animator>().SetBool("isSpell_1", true);
        try
        {
            GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
            locplayer.GetComponent<UnitSyncCmd>().CmdEffectBindUnit(2, Trig_Unit.gameObject);
            Trig_Unit.GetComponent<Self_class>().s_speed += 20;
            if (Trig_Unit.GetComponent<Self_class>().s_Controler == "User")
            {
                Trig_Unit.GetComponent<player>().JumpSpeed += 90;
                Trig_Unit.GetComponent<player>().Move();
            }
            else
            {
                Trig_Unit.GetComponent<Monster>().VertSpeed += 90;
                Trig_Unit.GetComponent<Monster>().Move();
            }
            Trig.GetComponent<player>().ig += DesXiaoGuo;
            Trig.GetComponent<player>().iw += DesXiaoGuoWater;
        }
        catch { }
    }
    void DesXiaoGuo()
    {
        Trig.GetComponent<UnitSyncCmd>().CmdDes(Trig.transform.FindChild("BU2").gameObject);
        Trig.GetComponent<Self_class>().s_speed -= 20;
        Trig.GetComponent<player>().ig -= DesXiaoGuo;
        Trig.GetComponent<player>().iw -= DesXiaoGuoWater;
    }
    void DesXiaoGuoWater()
    {
        Trig.GetComponent<UnitSyncCmd>().CmdDes(Trig.transform.FindChild("BU2").gameObject);
        Trig.GetComponent<Self_class>().s_speed -= 20;
        Trig.GetComponent<player>().ig -= DesXiaoGuo;
        Trig.GetComponent<player>().iw -= DesXiaoGuoWater;
    }
}
