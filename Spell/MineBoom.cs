using UnityEngine;
using System.Collections;

public class MineBoom : MonoBehaviour {
    public Transform effect;
    private Transform Mine;
    private Transform Scan_Model;
    private Vector3 pos;
    private Vector3 eua;
    private Vector3 rot_1;
    private Vector3 rot_2;
    private bool isCover;
    private Transform[] Target = new Transform[20];
    private int m;
    private float radius;
    private float del_angle;
    private int scan_count;
    private RaycastHit hit;
    private bool isWork=false;
    void Start()
    {
        Mine = this.transform;
        Scan_Model = this.transform.FindChild("Scan");
        radius = 4;
        del_angle = 10 * Mathf.PI / 180;
        scan_count = (int)(2 * Mathf.PI / del_angle);
        pos = Mine.position;
        eua = Mine.eulerAngles;
        m = 0;
        isWork = true;
    }
    void FixedUpdate()
    {
        if (isWork)
        {
            Scan();
            if (m>0)
            {
                Bomb();
            }
        }
    }
    void Bomb()
    {
        Instantiate(effect, Mine.position, Mine.rotation);
        for (int i = 0; i < m; i++)
        {
            if (Target[i].GetComponent<Self_class>().s_class == "Hero" && Target[i].GetComponent<Self_class>().isLife == true)
            {
                Target[i].GetComponent<Self_class>().injured(300, Mine.GetComponent<Excre_class>().ownner);
            }
        }
        Mine.GetComponent<Excre_class>().Des();
    }
    void Scan()
    {
        for (int k = 0; k < scan_count; k++)
        {
            rot_2 = Scan_Model.right * (k * 180 * del_angle / Mathf.PI);
            for (int j = 0; j < scan_count; j++)
            {
                rot_1 = Scan_Model.up * (j * 180 * del_angle / Mathf.PI);
                Scan_Model.Rotate(rot_1);
                Scan_Model.Rotate(rot_2);
                if (Physics.Linecast(pos, pos + radius * Scan_Model.forward, out hit))
                {
                    isCover = false;
                    if(hit.collider.transform.GetComponent<Self_class>() != null)
                    {
                        if(hit.collider.transform.GetComponent<Self_class>().s_class!="Hero"|| hit.collider.transform.GetComponent<Self_class>().s_class != "Monster")
                        {
                            isCover = true;
                        }
                        else
                        {
                            if (hit.collider.transform == Mine.GetComponent<Excre_class>().ownner)
                            {
                                isCover = true;
                            }
                            else
                            {
                                foreach (Transform ta in Target)
                                {
                                    if (hit.collider.transform == ta)
                                    {
                                        isCover = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        isCover = true;
                    }
                    if (!isCover)
                    {
                        Target[m] = hit.collider.transform;
                        m++;
                    }
                }
                Scan_Model.eulerAngles = eua;
            }
        }
    }
}
