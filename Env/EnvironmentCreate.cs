using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class EnvironmentCreate : NetworkBehaviour {
    public PoolModel[] Models;
    public static EnvironmentCreate _ins;
    void Awake()
    {
        _ins = this;
    }
    //-----------------------------
    public GameObject[] EvExtra;
    //----------------
    //ev1:sqbody
    private GameObject Model;  
    IEnumerator CreateNewMap()
    {
        for(int i = 0; i < Models.Length; i++)
        {
            for(; Models[i].Count < Models[i].MaxCount; Models[i].Count++)
            {
                GameObject env = Instantiate(Models[i].prefab, RndPosGet(), Quaternion.identity, transform) as GameObject;
                env.GetComponent<MapEnvi>().Ake();
                NetworkServer.Spawn(env);
                yield return 0;
            }
        }
    }
    public void CreateEnv()
    {
        StartCoroutine(CreateNewMap()); 
    }
    //private IEnumerator CmdEnvInit()
    //{
    //    yield return StartCoroutine(EnvProPool.Ins.EnvInit());
    //    for(int i = 0; i < EnvId.Length; i++)
    //    {
    //        for (int j = 0; j < EnvCount[i]; j++)
    //        {
    //            GameObject env = EnvProPool.Ins.NewPro(EnvId[i]);
    //            env.transform.position = RndPosGet();
    //            env.transform.parent = this.transform;
    //            env.transform.rotation = Quaternion.identity;
    //            env.GetComponent<MapEnvi>().Ake();
    //            env.gameObject.SetActive(true);
    //        }
    //    }
    //}
    [Command]private void CmdDeleteElement(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }
    Vector3 RndPosGet()
    {
        float pos_x = Random.Range(105, 425);
        float pos_z = Random.Range(65, 425);
        float pos_y = Terrain.activeTerrain.SampleHeight(new Vector3(pos_x, 0, pos_z));
        Vector3 Rndpos = new Vector3(pos_x, pos_y, pos_z);
        return Rndpos;
    }
    [Command]public void CmdCreateAlong(int EnvIndex,Vector3 pos)
    {
        GameObject env = Instantiate(EvExtra[EnvIndex], pos, Quaternion.identity, transform) as GameObject;
        //env.GetComponent<MapEnvi>().Ake();
        NetworkServer.Spawn(env);
    }
    public void EnvBeGather(GameObject EnvElement)
    {
        EnvElement.GetComponent<EnvItemBe>().BeItem();                          
    }
}
