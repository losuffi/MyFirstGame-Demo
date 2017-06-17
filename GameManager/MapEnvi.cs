using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class MapEnvi : NetworkBehaviour {
    public  void Ake()
    {
        if (transform.GetComponent<Self_class>().s_class == "Tree")
        {
            this.transform.parent = GameObject.Find("Environment").transform.FindChild("Environ_Center");
        }
        else if(transform.GetComponent<Self_class>().s_class == "Monster")
        {
            this.transform.parent = GameObject.Find("Hero").transform.FindChild("Monster_Center");
        }
        this.name = this.name.Substring(0, this.name.Length - 7) + netId;
    }
}
