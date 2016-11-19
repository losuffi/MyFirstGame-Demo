using UnityEngine;
using System.Collections;

public class Granade_boom : MonoBehaviour {
    public Transform effect_Boom;
    public Transform effect_trail;
    private Transform Granade;
    private Transform Granade_appearance;
    private float Init_val;
    private float Init_VertSpeed;
    private float Init_HoriSpeed;
    private float Angles;
    private float Gravity_Acc;
    private float MaxHoriShift;
    private bool isWorking=false;
    public void Work(float val)
    {
        Gravity_Acc = 9;
        Angles = 30 * Mathf.PI / 180;
        MaxHoriShift = 50 * val;
        Init_val = Mathf.Sqrt((MaxHoriShift * Gravity_Acc) / (2 * Mathf.Cos(Angles) * Mathf.Sin(Angles)));
        Init_HoriSpeed = Init_val * Mathf.Cos(Angles);
        Init_VertSpeed = Init_val * Mathf.Sin(Angles);
        isWorking = true;
        Granade = this.transform;
        Granade_appearance = Granade.FindChild("Appearance");
        Transform ef = (Transform)Instantiate(effect_trail, Granade_appearance.position, Granade_appearance.rotation);
        ef.parent = Granade_appearance;
        ef.name = "Trail";
        if (!Granade.GetComponent<CharacterController>().isGrounded)
        {
            this.transform.GetComponent<CharacterController>().Move(Granade.up * 3);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag!="Item"&&other.transform != this.transform.GetComponent<Excre_class>().ownner && other.transform != this.transform) 
        {
            Bomb();
        }
    }
    void Update()
    {
        if (isWorking)
        {
            if (!Granade.GetComponent<CharacterController>().isGrounded)
            {
                Init_VertSpeed -= Gravity_Acc * Time.deltaTime;
            }
            else
            {
                Init_VertSpeed = 0;
                isWorking = false;
                Bomb();
            }
            Granade_appearance.Rotate(Granade_appearance.right * 120 * Time.deltaTime);
            Granade_appearance.FindChild("Trail").Rotate(Granade_appearance.right * 120 * Time.deltaTime);
            Vector3 dir = (Init_VertSpeed * Time.deltaTime * Granade.up) + (Init_HoriSpeed * Time.deltaTime * Granade.forward);
            Granade.GetComponent<CharacterController>().Move(dir);
        }
    }
    void Bomb()
    {
        float radius = 10;
        float del_angle = 10 * Mathf.PI / 180;
        int scan_count = (int)(2 * Mathf.PI / del_angle);
        Vector3 pos = Granade.position;
        Vector3 eua = Granade.eulerAngles;
        Vector3 rot_1;
        Vector3 rot_2;
        RaycastHit hit;
        bool isCover;
        Transform[] Target = new Transform[20];
        int m=0;
        for(int k = 0; k < scan_count; k++)
        {
            rot_2 = Granade.right * (k*180 * del_angle / Mathf.PI);
            for (int j = 0; j < scan_count; j++)
            {
                rot_1=Granade.up * (j*180 * del_angle / Mathf.PI);
                Granade.Rotate(rot_1);
                Granade.Rotate(rot_2);
                isCover = false;
                if (Physics.Linecast(pos, pos + radius * Granade.forward, out hit))
                {
                    if (hit.collider.transform.GetComponent<Self_class>() != null)
                    {
                        if (hit.collider.transform.GetComponent<Self_class>().s_class == "Hero" || hit.collider.transform.GetComponent<Self_class>().s_class == "Monster")
                        {
                            foreach (Transform ta in Target)
                            {
                                if (ta == hit.collider.transform || hit.collider.transform == Granade)
                                {
                                    isCover = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            isCover = true;
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
                Granade.eulerAngles = eua;
            }
        }
        Instantiate(effect_Boom, Granade.position, Granade.rotation);
        for(int i = 0; i < m; i++)
        {
            if (Target[i].GetComponent<Self_class>().isLife == true)
            {
                Target[i].GetComponent<Self_class>().injured(200, Granade.GetComponent<Excre_class>().ownner);
            }
        }
        Granade.GetComponent<Excre_class>().Des();
    }
}
