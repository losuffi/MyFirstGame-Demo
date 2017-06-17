using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class MineBoom : NetworkBehaviour {
    private Transform Mine;
    private Transform Scan_Model;
    private bool isCover;
    private Transform[] Target = new Transform[20];
    private int m;
    private RaycastHit hit;
    public bool isWork=false;
    private GameObject Owner;
    void Start()
    {
        transform.position += 1 * transform.up;
    }
    void Init()
    {
        Mine = this.transform;
        Scan_Model = this.transform.FindChild("Scan");
        m = 0;
        if (transform.GetComponent<Excre_class>().ownner != GameObject.Find("Hero").transform.FindChild("Player"))
        {
            Mine.FindChild("Mine_boom").gameObject.layer = 8;
        }
        Owner = transform.GetComponent<Excre_class>().ownner.gameObject;
        isWork = true;
        Scan();
    }
    void FixedUpdate()
    {
        if (isWork&&isServer)
        {
            Scan();
            if (m>0)
            {
                Bomb();
            }
        }
        else
        {
            if (transform.GetComponent<Excre_class>().ownner != null)
            {
                Init();
            }
        }
    }
    void Bomb()
    {
        for (int i = 0; i < m; i++)
        {
            Target[i].GetComponent<Self_class>().injured(300, Mine.GetComponent<Excre_class>().ownner);
        }
        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdEffectBindUnit(4, Target[0].gameObject);
        m = 0;
        Mine.GetComponent<Excre_class>().Des();
    }
    void Scan()
    {
        RaycastHit hit;
        Vector3 eu2 = Scan_Model.eulerAngles;
        for (int j = 0; j < 20; j++)
        {
            Scan_Model.Rotate(18 * j * Scan_Model.up);
            if (Physics.Raycast(Scan_Model.position, Scan_Model.forward, out hit, 4))
            {
                if (hit.collider.tag == "Living" && hit.transform.GetComponent<Self_class>() != null && hit.collider.gameObject != Owner)
                {
                    if (hit.transform.GetComponent<Self_class>().isLife)
                    {
                        bool isCover = false;
                        foreach (Transform ta in Target)
                        {
                            if (ta == hit.transform)
                            {
                                isCover = true;
                                break;
                            }
                        }
                        if (!isCover)
                        {
                            Target[m] = hit.transform;
                            m++;
                        }
                    }
                }
            }
            Scan_Model.eulerAngles = eu2;
        }
    }
}
