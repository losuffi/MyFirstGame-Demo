using UnityEngine;
using System.Collections;

public class Arrow_ : MonoBehaviour {
    public Transform Model;
    public void Work(Transform Trig_Unit)
    {
        Transform bullet;
        Trig_Unit.GetComponent<Animator>().SetBool("isSpell_1", true);
        bullet = (Transform)Instantiate(Model, Trig_Unit.position, Trig_Unit.rotation);
        bullet.GetComponent<Bullent>().Trig_Unit = Trig_Unit;
        bullet.parent = Trig_Unit.parent.FindChild("Excrement");
        bullet.GetComponent<Excre_class>().ownner = Trig_Unit;
        bullet.GetComponent<Excre_class>().LiveTime = 5;
        bullet.GetComponent<Excre_class>().Type = "Shot";
        bullet.GetComponent<Excre_class>().Name = "Bullet_A";
    }
}
