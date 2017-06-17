using UnityEngine;
using System.Collections;
using GamePoor;
public class EnvProPool : MonoBehaviour {
    private static EnvProPool ins;
    public static EnvProPool Ins
    {
        get { return ins; }
    }
    public GameObject[] prefab;
    public int[] count;
    void Awake()
    {
        ins = this;
    }
    private Poor myPoor = new Poor();
    public void RemovePro(GameObject obj)
    {
        if (!obj.GetComponent<Self_class>()||!myPoor.IsPoorObj(obj))
        {
            Destroy(obj);
            return;
        }
        if (obj.GetComponent<NavMeshAgent>() != null)
            obj.GetComponent<NavMeshAgent>().enabled = false;
        myPoor.Rem(obj);
    }
    public GameObject NewPro(int Id)
    {
        return myPoor.New(Id);
    }
    public IEnumerator EnvInit()
    {
        if (prefab.Length == count.Length)
        {
            for (int i = 0; i < prefab.Length; i++)
            {
                Pro temp = new Pro();
                temp.SetPro(prefab[i]);
                temp.WorkPro(count[i]);
                myPoor.AddPro(temp);
            }
        }
        yield return 0;
    }
}
