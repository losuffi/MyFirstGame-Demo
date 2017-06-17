using UnityEngine;
using System.Collections;

public class MonEnvBe : MonoBehaviour {
    public int EvIndex;
    public void BeItem()
    {
        StartCoroutine(work());
    }
    IEnumerator work()
    {
        yield return new WaitForSeconds(1f);
        EnvironmentCreate._ins.CmdCreateAlong(EvIndex, this.transform.position);
        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdDes(this.gameObject);
    }
}
