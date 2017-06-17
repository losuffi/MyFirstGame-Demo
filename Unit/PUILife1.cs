using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PUILife1 : MonoBehaviour {
    private GameObject unit;
    void Start()
    {
        unit = transform.parent.parent.gameObject;
    }
    void Update()
    {
        if (unit.GetComponent<Self_class>() != null)
        {
             transform.GetComponent<Slider>().value=(float)unit.GetComponent<Self_class>().p_life / unit.GetComponent<Self_class>().s_life;
        }
    }
}
