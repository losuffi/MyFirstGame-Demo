using UnityEngine;
using System.Collections;
public class HandAttack : MonoBehaviour
{
    public void Work(Transform Trig_Unit)
    {
        int m = 0;
        Transform[] Target = new Transform[10];
        int n = Random.Range(1, 5);
        AnimatorStateInfo As=Trig_Unit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(1);
        if (!As.IsName("Wait"))
        {
            Trig_Unit.GetComponent<player>().IsAttack = true;
            Trig_Unit.GetComponent<Animator>().SetInteger("AttackKind", 0);
        }
        else
        {
            Trig_Unit.GetComponent<Animator>().SetInteger("AttackKind", n);
            StartCoroutine(scanstate(Target, m, Trig_Unit));
        }
    }
    private IEnumerator Work_on(Transform []Target,int m,Transform Trig_Unit)
    {
        Vector3 Trig_point = Trig_Unit.position;
        RaycastHit hit;
        bool isCover = false;
        for (int j = 0; j < 3; j++)
        {
            for (int k = 0; k < 3; k++)
            {
                Trig_point = Trig_Unit.position + 0.3f * ((j - 1) * Trig_Unit.right + k * Trig_Unit.up);
                if (Physics.Raycast(Trig_point, _spellLib.AimAt.DirctAimV3(Trig_Unit.gameObject), out hit,2))
                {
                    isCover = false;
                    foreach (Transform ta in Target)
                    {
                        if (ta == hit.collider.transform || hit.collider.transform == Trig_Unit)
                        {
                            isCover = true;
                            break;
                        }
                    }
                    if (!isCover)
                    {
                        Target[m] = hit.collider.transform;
                        m++;
                    }

                }
            }
        }
        for (int n = 0; n < m; n++)
        {
            if (Target[n].GetComponent<Self_class>() != null)
            {
                if (Target[n].GetComponent<Self_class>().s_class != "Item" && Target[n].GetComponent<Self_class>().isLife == true)
                {
                    Target[n].GetComponent<Self_class>().injured(Trig_Unit.GetComponent<Self_class>().s_AttackValue, Trig_Unit);
                }
            }
            else if (Target[n].tag == "Environment" && Target[n].parent.GetComponent<Self_class>() != null&& Target[n].parent.GetComponent<Self_class>().s_class != "Item")
            {
                Target[n].parent.GetComponent<Self_class>().CmdGather(Trig_Unit.GetComponent<Self_class>().s_GatherValue, Trig_Unit);
            }
            else if(Target[n].tag == "Living")
            {
                if (Target[n].parent.GetComponent<Self_class>() != null)
                {
                    Target[n].parent.GetComponent<Self_class>().injured(Trig_Unit.GetComponent<Self_class>().s_AttackValue, Trig_Unit);
                }
            }
        }
        Trig_Unit.GetComponent<player>().IsAttack = true;
        StopAllCoroutines();
        yield return 0;
    }
    IEnumerator scanstate(Transform[] Target, int m, Transform Trig_Unit)
    {
        AnimatorStateInfo As = new AnimatorStateInfo();
        while (true)
        {
            As = Trig_Unit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(1);
            for (int i = 1; i < 5; i++)
            {
                if (As.IsName("A" + i) && As.normalizedTime >= 0.7)
                {
                    Trig_Unit.GetComponent<Animator>().SetInteger("AttackKind", 0);
                    StartCoroutine(this.Work_on(Target, m, Trig_Unit));
                    break;
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}
