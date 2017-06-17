using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GamePoor
{
    public class Pro
    {
        private int prosid;
        private GameObject prefab;
        private GameObject InitObj;
        private int Length;
        private Stack<GameObject> stuck = new Stack<GameObject>();
        private Component proc;
        public void SetPro(GameObject obj)
        {
            prefab = obj;
        }
        public void WorkPro(int length)
        {
            Length = length;
            InitObj = MonoBehaviour.Instantiate<GameObject>(prefab);
            if (InitObj.GetComponent<Self_class>())
                prosid = InitObj.GetComponent<Self_class>().s_id;
            UnityEngine.Networking.NetworkServer.Spawn(InitObj);
            InitObj.SetActive(false);
            for(int i = 0; i < length; i++)
            {
                GameObject temp = MonoBehaviour.Instantiate<GameObject>(prefab);
                temp.transform.SetParent(GameObject.Find("System/GamePoor").transform);
                temp.name = prosid + "/" + stuck.Count;
                UnityEngine.Networking.NetworkServer.Spawn(temp);
                stuck.Push(temp);
                temp.SetActive(false);
            }
        }
        public bool IsThisId(int Id)
        {
            return Id == prosid;
        }
        public bool IsEmpty
        {
            get
            {
                return stuck.Count == 0;
            }
        }
        public void AddObj(GameObject obj)
        {
            if (stuck.Count == Length)
            {
                MonoBehaviour.Destroy(obj); 
            }
            Reset(obj);
            stuck.Push(obj);
            obj.SetActive(false);
        }
        void Reset(GameObject obj)
        {
            obj.transform.SetParent(GameObject.Find("System/GamePoor").transform);
            obj.name = prosid + "/" + stuck.Count;
            Self_class temp = obj.GetComponent<Self_class>();
            Self_class pre = InitObj.GetComponent<Self_class>();
            temp.p_life = pre.p_life;
        }
        public void InsObj()
        {
            GameObject temp = MonoBehaviour.Instantiate<GameObject>(prefab);
            temp.transform.SetParent(GameObject.Find("System/GamePoor").transform);
            temp.name = prosid + "/" + stuck.Count;
            UnityEngine.Networking.NetworkServer.Spawn(temp);
            stuck.Push(temp);
            temp.SetActive(false);
        }
        public GameObject NewObj()
        {
            if (IsEmpty)
            {
                InsObj();
            }
            return stuck.Pop();   
        }
    }
    public class Poor
    {
        private List<Pro> poorspace = new List<Pro>();
        public void AddPro(Pro temp)
        {
            poorspace.Add(temp);
        }
        public void Rem(GameObject obj)
        {
            if (!obj.GetComponent<Self_class>())
                return;
            Pro currentpro = poorspace.Find(alist =>
             {
                 return alist.IsThisId(obj.GetComponent<Self_class>().s_id);
             });
            if (currentpro == null)
                return;
            currentpro.AddObj(obj);
            obj.SetActive(false);
        }
        public GameObject New(int ID)
        {
            Pro currentpro = poorspace.Find(alist =>
            {
                return alist.IsThisId(ID);
            });
            if (currentpro == null)
                return null;
            return currentpro.NewObj();
        }
        public bool IsPoorObj(GameObject obj)
        {
            foreach(Pro p in poorspace)
            {
                if (p.IsThisId(obj.GetComponent<Self_class>().s_id))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
