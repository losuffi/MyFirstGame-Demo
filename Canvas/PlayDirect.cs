using UnityEngine;
using System.Collections;

public class PlayDirect : MonoBehaviour {
    public Transform cplayer;
    public void Work(byte n)
    {
        cplayer.GetComponent<player>().Behavior(n);
    }
}
