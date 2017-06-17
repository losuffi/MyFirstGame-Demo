using UnityEngine;
using System.Collections;

public class Arrow_2 : MonoBehaviour
{
    public void Work(Transform Trig_Unit)
    {
        Trig_Unit.GetComponent<Animator>().SetBool("isSpell_1", true);
        GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        locplayer.GetComponent<UnitSyncCmd>().CmdBullet(Trig_Unit.gameObject, 2);
    }
}

