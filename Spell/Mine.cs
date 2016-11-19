using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
    public Transform Model;
    public void Work(Transform Trig_Unit)
    {
        StartCoroutine(Chanshen(Trig_Unit));
    }
    IEnumerator Chanshen(Transform Trig_Unit)
    {
        yield return new WaitForSeconds(0.4f);
        Transform Mine;
        Trig_Unit.GetComponent<Animator>().SetBool("isSpell_1", true);
        Mine = (Transform)Instantiate(Model, Trig_Unit.position, Trig_Unit.rotation);
        Mine.parent = Trig_Unit.parent.FindChild("Excrement");
        Mine.name = "Mine_boom";
        Mine.GetComponent<Excre_class>().ownner = Trig_Unit;
        Mine.GetComponent<Excre_class>().LiveTime = 5 * 60;
        Mine.GetComponent<Excre_class>().Type = "t1";
        Mine.GetComponent<Excre_class>().Name = "地雷";
    }
}
