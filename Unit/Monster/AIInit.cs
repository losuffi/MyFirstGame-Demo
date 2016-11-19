using UnityEngine;
using System.Collections;

public class AIInit : MonoBehaviour {
    public Transform Brain;
    public Transform Inductor;
    public Transform Mo;
    void Awake()
    {
        Mo = this.transform.parent;
        Brain = this.transform.FindChild("Brain");
        Inductor = this.transform.FindChild("Inductor");
    }
}
