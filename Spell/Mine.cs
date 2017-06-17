using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {
    public void Work(Transform Trig_Unit)
    {
        StartCoroutine(Chanshen(Trig_Unit));
    }
    IEnumerator Chanshen(Transform Trig_Unit)
    {
        yield return new WaitForSeconds(0.4f);
        Trig_Unit.GetComponent<Animator>().SetBool("isSpell_1", true);
        GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        locplayer.GetComponent<UnitSyncCmd>().CmdMine(Trig_Unit.gameObject);
    }
}
