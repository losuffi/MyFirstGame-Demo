using UnityEngine;
using System.Collections;

public class Granades : MonoBehaviour {
    public Transform Model;
    public void Work(Transform Trig_Unit)
    {
        if (Trig_Unit.GetComponent<Self_class>().isCasting)
        {
            float val = Trig_Unit.GetComponent<Self_class>().s_Canvas.transform.FindChild("Bars").GetComponent<Bars_i>().End();
            Transform granade;
            granade = (Transform)Instantiate(Model, Trig_Unit.position, Trig_Unit.rotation);
            granade.parent = Trig_Unit.parent.FindChild("Excrement");
            granade.name = "Gradade_boom";
            granade.GetComponent<Excre_class>().ownner = Trig_Unit;
            granade.GetComponent<Excre_class>().LiveTime = 20;
            granade.GetComponent<Excre_class>().Type = "t2";
            granade.GetComponent<Excre_class>().Name = "炸弹";
            granade.GetComponent<Granade_boom>().Work(val);
            Trig_Unit.GetComponent<Self_class>().isCasting = false;
        }
        else
        {
            Trig_Unit.GetComponent<Self_class>().s_Canvas.transform.FindChild("Bars").GetComponent<Bars_i>().work(2);
            Trig_Unit.GetComponent<Self_class>().isCasting = true;
        }
    }
}
