using UnityEngine;
using System.Collections;

public class MeatSmall : MonoBehaviour {
    public Transform effect;
    public void Work(Transform Trig_Unit,float value)
    {
        Trig_Unit.GetComponent<Self_class>().ChanHunger(value);
        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdEffectBindUnit(6, Trig_Unit.gameObject);
    }
}
