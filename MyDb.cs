using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using GetSName;
public sealed class MyDb : MonoBehaviour {
    public GameObject[] Weapon;  //武器模型數據組
    public GameObject BuildingPrefab;
    public GameObject[] Build;  //建筑模型数据组
    public GameObject[] Sth; //杂物模型数据组，如篝火火焰这种
    public GameObject[] effect;
    public GameObject Norenderobj;
    public Dictionary<int, ItemTemp> ItemDB = new Dictionary<int, ItemTemp>();
    void Start()
    {
        Item.StartItemData();
    }
}
