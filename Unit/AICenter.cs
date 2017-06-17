using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AICenter : MonoBehaviour {
    private static AICenter _ins;
    public static AICenter Ins
    {
        get { return _ins; }
    }
    private int Count = 0;
    public int MaxIndex
    {
        get
        {
            return (Count-1);
        }
    }
    private Dictionary<int, GameObject> map = new Dictionary<int, GameObject>();
    void Inputplayer(GameObject obj)
    {
        map.Add(Count, obj);
        Count++;
    }
    public GameObject Getplayer(int index)
    {
        return map[index];
    }
    void Awake()
    {
        _ins = this;
    }
    void Start()
    {
        GameManager._ins.Insplayer += Inputplayer;
    }
}
