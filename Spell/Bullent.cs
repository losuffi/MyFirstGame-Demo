using UnityEngine;
using System.Collections;

public class Bullent : MonoBehaviour {
    public Transform Trig_Unit;
    private string typename;
    private Transform Inductor;
    private Transform Targer;
    private Transform owner;
    void Start()
    {
        this.transform.position += Vector3.up * 2;
        typename = this.GetComponent<Excre_class>().Name;
        Inductor = this.transform.FindChild("Scan");
        owner = this.GetComponent<Excre_class>().ownner;
    }

    void Update()
    {
        if (typename == "Bullet_A")
        {
            this.transform.position += transform.forward * 15 * Time.deltaTime;
        }
        else if(typename == "Bullet_B")
        {
            this.transform.position += transform.forward * 30 * Time.deltaTime;
        }
        if (IsShootUnit())
        {
            damage();
        }
    }
    private void damage()
    {
        try
        {
            if (typename == "Bullet_A")
            {
                Targer.GetComponent<Self_class>().injured(150, owner);
            }
            else if (typename == "Bullet_B")
            {
                Targer.GetComponent<Self_class>().injured(180, owner);
            }
            this.GetComponent<Excre_class>().Des();
        }
        catch
        {
            this.GetComponent<Excre_class>().Des();
        }
    }
    private bool IsShootUnit()
    {
        Vector3 eua = Inductor.eulerAngles;
        for (int i = 0; i < (360 / 5f); i++)
        {
            Inductor.Rotate(Inductor.right * 5f * i);
            Vector3 eua2 = Inductor.eulerAngles;
            for (int j=0;j< (360 / 5f); j++)
            {
                Inductor.Rotate(Inductor.up * 5f*j);
                RaycastHit hit;
                if (Physics.Raycast(Inductor.position, Inductor.forward, out hit, 2))
                {
                    if (hit.transform != owner)
                    {
                        if (hit.transform.GetComponent<Self_class>() != null && hit.transform != this.transform)
                        {
                            if (hit.transform.tag == "Living")
                            {
                                Targer = hit.transform;
                                return true;
                            }
                        }
                    }
                }
                Inductor.eulerAngles = eua2;
            }
            Inductor.eulerAngles = eua;
        }
        return false;
    }
}
