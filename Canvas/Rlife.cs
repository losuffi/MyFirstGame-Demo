using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Rlife : MonoBehaviour {
    private GameObject target;
    public void setTarget(GameObject unit)
    {
        target = unit;
        transform.FindChild("T").GetComponent<Text>().text = target.GetComponent<Self_class>().s_name;
    }
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    void Update()
    {
        if (target != null)
        {
            if (target.GetComponent<Self_class>() != null)
            {
                transform.GetComponent<Slider>().value = (float)target.GetComponent<Self_class>().p_life / target.GetComponent<Self_class>().s_life;
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
