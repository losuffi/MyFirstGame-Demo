using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Granade_boom : NetworkBehaviour {
    private GameObject[] Target = new GameObject[20];
    private Transform Granade;
    private Transform Granade_appearance;
    private Transform Scan;
    private float Init_val;
    private float Init_VertSpeed;
    private float Init_HoriSpeed;
    private float Angles;
    private float Gravity_Acc;
    private float MaxHoriShift;
    private int m = 0;
    public void Work(float val)
    {
        Gravity_Acc = 9;
        Angles = 30 * Mathf.PI / 180;
        MaxHoriShift = 50 * val;
        Init_val = Mathf.Sqrt((MaxHoriShift * Gravity_Acc) / (2 * Mathf.Cos(Angles) * Mathf.Sin(Angles)));
        Init_HoriSpeed = Init_val * Mathf.Cos(Angles);
        Init_VertSpeed = Init_val * Mathf.Sin(Angles);
        Granade = this.transform;
        Granade_appearance = Granade.FindChild("Appearance");
        Scan = Granade.FindChild("Scan");
        if (!Granade.GetComponent<CharacterController>().isGrounded)
        {
            this.transform.GetComponent<CharacterController>().Move(Granade.up * 3);
        }
        StartCoroutine(runtime());
    }
    IEnumerator runtime()
    {
        while (true)
        {
            if (!Granade.GetComponent<CharacterController>().isGrounded)
            {
                Init_VertSpeed -= Gravity_Acc * Time.deltaTime;
            }
            else
            {
                Init_VertSpeed = 0;
                Bomb();
                yield return 0;
            }
            Granade_appearance.Rotate((Granade_appearance.right * 20 + Granade_appearance.forward * 180) * Time.deltaTime);
            Vector3 dir = (Init_VertSpeed * Time.deltaTime * Granade.up) + (Init_HoriSpeed * Time.deltaTime * Granade.forward);
            Granade.GetComponent<CharacterController>().Move(dir);
            yield return new WaitForSeconds(0.02f);
        }
    }
    void Bomb()
    {
        if (!isServer) return;
        RaycastHit hit;
        Vector3 eua = Scan.eulerAngles;
        for(int i = 0; i < 18; i++)
        {
            for(int j = 0; j < 18; j++)
            {
                Scan.Rotate((i * 20 + 1) * Scan.right);
                Scan.Rotate((j * 20 + 1) * Scan.up);
                if(Physics.Linecast(Scan.position,Scan.position+10*Scan.forward,out hit))
                {
                    bool isCover = false;
                    if (hit.transform.tag == "Living")
                    {
                        foreach(GameObject ga in Target)
                        {
                            if (ga == hit.collider.gameObject)
                            {
                                isCover = true;
                                break;
                            }
                        }
                        if (!isCover)
                        {
                            Target[m] = hit.collider.gameObject;
                            hit.transform.GetComponent<Self_class>().injured(200, Granade.GetComponent<Excre_class>().ownner);
                            m++;
                        }
                    }
                }
                Scan.eulerAngles = eua;
            }
        }
        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdEffectBindPos(5, Granade_appearance.position);
        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdDes(this.gameObject);
    }
}
