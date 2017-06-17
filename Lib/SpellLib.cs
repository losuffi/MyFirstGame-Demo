using UnityEngine;
using System.Collections;
using System.Text;
namespace _spellLib
{
    class AimAt {
        public static Vector3 DirctAimV3(GameObject obj)
        {
            if (obj.GetComponent<player>().playcamera != null)
            {
                return obj.GetComponent<player>().playcamera.transform.forward;
            }
            else
            {
                return obj.transform.forward;
            }
        }
        public static Quaternion DirctAimQ(GameObject obj)
        {
            if (obj.GetComponent<player>().playcamera != null)
            {
                return obj.GetComponent<player>().playcamera.transform.rotation;
            }
            else
            {
                return obj.transform.rotation;
            }
        }
    }
    class Control
    {
        public static KeyCode StrTransKey(string str)
        {
            return (KeyCode)97;
        }
    }
    public class Bag
    {
        struct Item
        {
            public Item(int a,int b=1)
            {
                Id = a;
                Count = b;
                Iscast = false;
            }
            public int Id;
            public int Count;
            public bool Iscast;
        };
        public Bag(int _size)
        {
            Bagspace = new Hashtable();
            Size = _size;
            _NowSize = 0;
        }
        public void Push(GameObject obj)
        {
            if (obj.GetComponent<Self_class>() == null)
            {
                Debug.Log("Wrong Target!");
                return ;
            }
            if (_NowSize >= Size)
                return;
            Item objc = new Item(obj.GetComponent<Self_class>().s_id, obj.GetComponent<Self_class>().s_iCount);
            if (Bagspace.Contains(objc.Id))
            {
                Item temp = (Item)Bagspace[objc.Id];
                temp.Count += objc.Count;
                Bagspace[objc.Id] = temp;
            }
            else
            {
                _NowSize++;
                Bagspace.Add(objc.Id, objc);
            }
        }
        public void Delete(int Id)
        {
            if (!Bagspace.Contains(Id))
            {
                Debug.Log("Wrong Target");
                return;
            }
            Bagspace.Remove(Id);
            _NowSize--;
        }
        public void Sub(int Id,int subcount = 1)
        {
            if (!Bagspace.Contains(Id))
            {
                Debug.Log("Wrong Target");
                return;
            }
            Item temp = (Item)Bagspace[Id];
            if (temp.Count - subcount <= 0)
            {
                Delete(Id);
            }
            else
            {
                temp.Count -= subcount;
                Bagspace[Id] = temp;
            }
        }
        public bool Iscast(int Id)
        {
            Item temp= (Item)Bagspace[Id];
            return temp.Iscast;
        }
        public void Cast(int Id)
        {
            Item temp = (Item)Bagspace[Id];
            if (temp.Iscast)
            {
                Sub(Id);
                temp.Iscast = false;
            }
            else
            {
                temp.Iscast = true;
            }
            Bagspace[Id] = temp;
        }
        public int GetCount(int Id)
        {
            if (!Bagspace.Contains(Id))
            {
                return 0;
            }
            Item temp = (Item)Bagspace[Id];
            return temp.Count;
        }
        public GameObject GoEntity(int Id)
        {
            GameObject Entity = GameObject.Instantiate(GameObject.Find("System/MyDb").GetComponent<MyDb>().Norenderobj);
            Entity.GetComponent<Self_class>().s_id = Id;
            Entity.GetComponent<Self_class>().s_iType = GetSName.Item.Type(Id);
            Entity.GetComponent<Self_class>().s_Icontent= GetSName.Item.Content(Id);
            Entity.GetComponent<Self_class>().s_AttackValue = GetSName.Item.Attack(Id);
            Entity.GetComponent<Self_class>().s_name = GetSName.Item.Name(Id);
            Entity.GetComponent<Self_class>().s_GatherValue = GetSName.Item.GatherValue(Id);
            Entity.GetComponent<Self_class>().s_speed = GetSName.Item.Speed(Id);
            Entity.GetComponent<Self_class>().s_Defence = GetSName.Item.Defense(Id);
            Entity.GetComponent<Self_class>().s_life = GetSName.Item.Slife(Id);
            Entity.name = GetSName.Item.Name(Id);
            return Entity;
        }
        public int Nowsize
        {
            get
            {
                return _NowSize;
            }
        }
        public void SetOwner(GameObject obj)
        {
        }
        public void Clear()
        {
            Bagspace.Clear();
            _NowSize = 0;
        }
        public bool IsFull()
        {
            return _NowSize < Size ? false : true;
        }
        public bool Query(int Id)
        {
            return Bagspace.Contains(Id);
        }
        public ICollection SpaceValue()
        {
            return Bagspace.Values;
        }
        public ICollection SpaceKeys()
        {
            return Bagspace.Keys;
        }
        private Hashtable Bagspace;
        private int Size;
        private int _NowSize;
    }
    class MyCanvas
    {
        public static void IssueDis(string str)
        {
            GameObject.Find("Canvas/IssueBoard").GetComponent<IssueBoard>().dis(str);
        }
    }
    class SpellSystem {
        public static void SpellCast(int spellid,GameObject TrigUnit)
        {
            GameObject.Find("Hero/Spell").GetComponent<Spell_cast>().casting(spellid, TrigUnit.transform);
        } 
    }
    public class ShortCuts
    {
        public ShortCuts()
        {
            ShortSpace = new Hashtable();
        }
        public void AddShort(KeyCode _key,int ID)
        {
            KeyCode prek = GetKey(ID);
            if (prek != KeyCode.Joystick1Button19 && ShortSpace.Contains(prek))
            {
                ShortSpace.Remove(prek);
            }
            if (ShortSpace.Contains(_key))
            {
                ShortSpace[_key] = ID;
            }
            else
            {
                ShortSpace.Add(_key,ID);
            }
        }
        public void KeyTrig(KeyCode _key,GameObject Trigobj)
        {
            if (ShortSpace.Contains(_key))
            {
                Trigobj.GetComponent<player_pocket>().use((int)ShortSpace[_key]);
            }
        }
        public KeyCode GetKey(int Id)
        {
            if (ShortSpace.ContainsValue(Id))
            {
                ICollection sk = ShortSpace.Keys;
                foreach(KeyCode k in sk)
                {
                    if ((int)ShortSpace[k] == Id)
                    {
                        return k;
                    }
                }
            }
            return KeyCode.Joystick1Button19;
        }
        private Hashtable ShortSpace;
    }
    class SelfClass
    {
        public static void AddAtribute(GameObject obj,GameObject Aobj)
        {
            Self_class atc = Aobj.GetComponent<Self_class>();
            Self_class otc = obj.GetComponent<Self_class>();
            atc.s_AttackValue += otc.s_AttackValue;
            atc.s_Defence += otc.s_Defence;
            atc.s_GatherValue += otc.s_GatherValue;
            atc.s_life += otc.s_life;
            atc.s_speed += otc.s_speed;
        }
        public static void SubAtribute(GameObject obj, GameObject Aobj)
        {
            Self_class atc = Aobj.GetComponent<Self_class>();
            Self_class otc = obj.GetComponent<Self_class>();
            atc.s_AttackValue -= otc.s_AttackValue;
            atc.s_Defence -= otc.s_Defence;
            atc.s_GatherValue -= otc.s_GatherValue;
            atc.s_life -= otc.s_life;
            atc.s_speed -= otc.s_speed;
        }
    }
    class Talent
    {
        public static void Equi(int sid,GameObject player)
        {

        }
        public static void UnEqui(int sid, GameObject player)
        {

        }
    }
}

