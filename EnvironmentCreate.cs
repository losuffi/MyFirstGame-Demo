using UnityEngine;
using System.Collections;

public class EnvironmentCreate : MonoBehaviour {
    public int maxCountTree;
    public int CountTree=0;
    public Transform TreeMode;
    public Transform GrassMode;
    public int maxCountGrass;
    public int CountGrass = 0;
    public Transform RockMode;
    public int maxCountRock;
    public int CountRock = 0;
    public Transform AnimalMode;
    public int maxCountAnimal;
    public int CountAnimal = 0;
    void Start()
    {
        CreateTree();
        CreateGrass();
        CreateRock();
    }
    private void CreateTree()
    {
        while (CountTree < maxCountTree)
        {
            RndPosGet();
            Transform tree = (Transform)Instantiate(TreeMode, RndPosGet(), Quaternion.identity, this.transform);
            tree.name = "Tree" + CountTree;
            CountTree++;
        }
    }
    private void CreateGrass()
    {
        while (CountGrass < maxCountGrass)
        {
            RndPosGet();
            Transform Grass = (Transform)Instantiate(GrassMode, RndPosGet(), Quaternion.identity,this.transform);
            Grass.name = "Grass" + CountGrass;
            CountGrass++;
        }
    }
    private void CreateRock()
    {
        while (CountRock< maxCountRock)
        {
            RndPosGet();
            Transform Rock = (Transform)Instantiate(RockMode, RndPosGet(), Quaternion.identity, this.transform);
            Rock.name = "Rock" + CountGrass;
            CountRock++;
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
    public void CreateAlone(Transform Model,Vector3 pos)
    {
        Transform Mod = (Transform)Instantiate(Model, pos, Quaternion.identity, this.transform);
    }
    void FixedUpdate()
    {
        if (GameObject.Find("System").GetComponent<TimeTram>().NowTime[0] % 3 == 2)
        {
            CreateTree();
            CreateGrass();
        }
    }
}
