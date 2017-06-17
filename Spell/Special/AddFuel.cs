using UnityEngine;
using System.Collections;

public class AddFuel : MonoBehaviour
{
    public void Work(Transform Trig_Unit)
    {
        Trig_Unit.GetComponent<Animator>().SetBool("isSpell_1", true);
        Vector3 Trig_point = Trig_Unit.position;
        RaycastHit hit;
        for (int j = 0; j < 5; j++)
        {
            Trig_point = Trig_Unit.position + 0.4f*j * Trig_Unit.up;
            if (Physics.Raycast(Trig_point, Trig_Unit.forward,out hit,4))
            {
                if (hit.transform.name == "BonFire")
                {
                    hit.transform.GetComponent<BonFire>().AddFuel();
                    return;
                }
            }
        }
        GameObject.Find("Environment").transform.FindChild("Item_class").GetComponent<Item_create>().ItemAloneCreate(Trig_Unit.position, 20026);
    }
}
