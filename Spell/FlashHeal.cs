using UnityEngine;
using System.Collections;

public class FlashHeal : MonoBehaviour {
    public void Work(Transform Trig_Unit)
    {
        GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        locplayer.GetComponent<UnitSyncCmd>().CmdEffectBindUnit(1, Trig_Unit.gameObject);
        StartCoroutine(Work_on(Trig_Unit));
    }
    private IEnumerator Work_on(Transform Trig_Unit)
    {
        yield return new WaitForSeconds(1f);
        Trig_Unit.GetComponent<Self_class>().Heal(100);
        yield return new WaitForSeconds(1f);
        GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        locplayer.GetComponent<UnitSyncCmd>().CmdDes(Trig_Unit.FindChild("BU" + 1).gameObject);
    }
}
