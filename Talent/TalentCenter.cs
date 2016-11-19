using UnityEngine;
using System.Collections;

public class TalentCenter : MonoBehaviour {
    public void EquiWork(int Iid,Transform Trig_Unit)
    {
        if (Iid == 20016)
        {
            AddDefense(Trig_Unit, 20);
        }
        else if (Iid == 20017)
        {
            AddForce(Trig_Unit, 10);
        }
    }
    public void Work(int tid,Transform Trig_Unit)
    {
        if (tid == 50001)
        {
            AddForce(Trig_Unit,30);
        }
        else if (tid == 50002)
        {
            AddSpeed(Trig_Unit, 1.5f);
        }
        else if(tid == 50003)
        {
            AddDefense(Trig_Unit, 20);
        }

    }
    private void AddForce(Transform Trig_Unit,float forcevalue )
    {
        Trig_Unit.GetComponent<Self_class>().s_AttackValue += forcevalue;
    }
    private void AddSpeed(Transform Trig_Unit,float speedvalue)
    {
        Trig_Unit.GetComponent<Self_class>().s_speed += speedvalue;
    }
    private void AddDefense(Transform Trig_Unit, float defensevalue)
    {
        Trig_Unit.GetComponent<Self_class>().s_Defence += defensevalue;
    }
}
