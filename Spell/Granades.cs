using UnityEngine;
using System.Collections;

public class Granades : MonoBehaviour {
    public void Work(Transform Trig_Unit)
    {
        if (Trig_Unit.GetComponent<Self_class>().isCasting)
        {
            float val = Trig_Unit.GetComponent<Self_class>().s_Canvas.transform.FindChild("Bars").GetComponent<Bars_i>().End();
            GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdGranade(Trig_Unit.gameObject, val);
            Trig_Unit.GetComponent<Self_class>().isCasting = false;
        }
        else
        {
            Trig_Unit.GetComponent<Self_class>().s_Canvas.transform.FindChild("Bars").GetComponent<Bars_i>().work(2);
            Trig_Unit.GetComponent<Self_class>().isCasting = true;
        }
    }
}
