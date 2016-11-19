using UnityEngine;
using System.Collections;

public class MonEnvBe : MonoBehaviour {
    public Transform EnvModel;
    private Transform EnvCenter;
    void Awake()
    {
        EnvCenter = GameObject.Find("Environment").transform.FindChild("Environ_Center");
    }
    public void BeItem()
    {
        StartCoroutine(work());
    }
    IEnumerator work()
    {
        yield return new WaitForSeconds(1f);
        EnvCenter.GetComponent<EnvironmentCreate>().CreateAlone(EnvModel, this.transform.position);
        Destroy(this.transform.gameObject);
    }
}
