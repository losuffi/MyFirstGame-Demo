using UnityEngine;
using System.Collections;

public class MonsterCenter : MonoBehaviour {
    public int maxCountSq;
    public int CountSq = 0;
    public Transform SqMode;
    public void CreateMon()
    {
        CreateSq();
    }
    private void CreateSq()
    {
        while (CountSq < maxCountSq)
        {
            RndPosGet();
            Transform tree = (Transform)Instantiate(SqMode, RndPosGet(), Quaternion.identity);
            tree.parent = this.transform;
            tree.name = "Sq" + CountSq;
            CountSq++;
        }
    }
    Vector3 RndPosGet()
    {
        float pos_x = Random.Range(2, 154);
        float pos_z = Random.Range(2, 154);
        float pos_y = Terrain.activeTerrain.SampleHeight(new Vector3(pos_x, 0, pos_z));
        Vector3 Rndpos = new Vector3(pos_x, pos_y, pos_z);
        return Rndpos;
    }
}
