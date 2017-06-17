using UnityEngine;
using System.Collections;

public class Lobbywait : MonoBehaviour {
    public GameObject WaitCanvas;
    public void Ks()
    {
        Instantiate(WaitCanvas);
        try
        {
            GameObject.Find("NUI").SetActive(false);
        }
        catch { }
    }
}
