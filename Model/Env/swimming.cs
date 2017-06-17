using UnityEngine;
using System.Collections;

public class swimming : MonoBehaviour {
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Living")
        {
            if (other.gameObject.GetComponent<player>())
                other.gameObject.GetComponent<player>().Swim();
        }
    }
}
