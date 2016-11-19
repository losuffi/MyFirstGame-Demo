using UnityEngine;
using System.Collections;

public class MeatSmall : MonoBehaviour {
    public Transform effect;
    public void Work(Transform Trig_Unit,float value)
    {
        Trig_Unit.GetComponent<Self_class>().ChanHunger(value);
        Instantiate(effect, Trig_Unit.position, Trig_Unit.rotation, Trig_Unit);
    }
}
