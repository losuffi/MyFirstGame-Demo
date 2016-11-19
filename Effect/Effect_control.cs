using UnityEngine;
using System.Collections;
public class Effect_control : MonoBehaviour {
    void FixedUpdate()
    {
        if (this.GetComponent<ParticleSystem>().isPlaying)
        {
        }
        else
        {
            this.GetComponent<ParticleSystem>().Play();
        }
    }
}
