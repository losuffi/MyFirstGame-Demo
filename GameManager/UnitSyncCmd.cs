using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class UnitSyncCmd : NetworkBehaviour {
    public GameObject ItemModel;
    public GameObject BulletModel, Mine, Granade;
    private GameObject efModel;
    public GameObject[] Weapon;
    //--------------------------------------------!
    // ef1:Heal ef_2:RocketEjector ef3:FlameSpray ef4:Mine ef5:Grande;
    //----------------------------------------------Serve
    [Command]
    public void CmdAddCount(GameObject obj,int count)
    {
        obj.GetComponent<Self_class>().s_iCount += count;
    }
    [Command]
    public void CmdSetCount(GameObject obj,int count)
    {
        obj.GetComponent<Self_class>().s_iCount = count;
    }
    [Command]
    public void CmdInsWeapon(int weaponIndex,Vector3 pos,Quaternion qua,GameObject ownner)
    {
        GameObject Test = GameObject.Find("System/MyDb").GetComponent<MyDb>().Weapon[weaponIndex];
        GameObject obj = Instantiate(Test, pos, qua) as GameObject;
        obj.GetComponent<InsBass>().obj = ownner;
        obj.GetComponent<InsBass>().isInit = true;
        NetworkServer.Spawn(obj);
    }
    [Command]
    public void CmdInsSth(int SthIndex,Vector3 pos,Quaternion qua,GameObject owner)
    {
        GameObject Test = GameObject.Find("System/MyDb").GetComponent<MyDb>().Sth[SthIndex];
        GameObject obj = Instantiate(Test, pos, qua) as GameObject;
        obj.GetComponent<InsSth>().obj = owner;
        obj.GetComponent<InsSth>().isInit = true;
        NetworkServer.Spawn(obj);
    }
    [Command]
    public void CmdBuilding(int BuildIndex,Vector3 pos, Quaternion qua, GameObject parent, GameObject Owner)
    {
        GameObject mod = GameObject.Find("System").transform.FindChild("MyDb").gameObject.GetComponent<MyDb>().Build[BuildIndex];
        GameObject InsObj = Instantiate(mod, pos, qua, parent.transform) as GameObject;
        InsObj.name = InsObj.GetComponent<Self_class>().s_name;
        InsObj.GetComponent<Self_class>().Owner = Owner;
        NetworkServer.Spawn(InsObj);
    }
    [Command]
    public void CmdDes(GameObject obj)
    {
        Destroy(obj);
        //EnvProPool.Ins.RemovePro(obj);
    }
    [Command]
    public void CmdChangeLife(float changevalue, GameObject ply)
    {
        ply.GetComponent<Self_class>().p_life += changevalue;
    }
    [Command]
    public void CmdLifeInit(GameObject ply)
    {
        ply.GetComponent<Self_class>().p_life = ply.GetComponent<Self_class>().s_life;
    }
    [Command]
    public void CmdLifeZero(GameObject ply)
    {
        ply.GetComponent<Self_class>().p_life = 0;
    }
    [Command]
    public void CmdItemCreate(int ItemId, int ItemCount, Vector3 pos)
    {
        float posx = pos.x;
        float posz = pos.z;
        float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 1, posz)) + 2;
        pos = new Vector3(posx, posy, posz);
        GameObject clone = Instantiate(ItemModel, pos, Quaternion.identity) as GameObject;
        clone.GetComponent<Self_class>().s_id = ItemId;
        clone.GetComponent<Self_class>().s_iCount = ItemCount;
        clone.GetComponent<Self_class>().isPick = false;
        NetworkServer.Spawn(clone);
    }
    [Command]
    public void CmdItemCountChange(GameObject Item,int changevalue)
    {
        Item.GetComponent<Self_class>().s_iCount += changevalue;
    }
    [Command]
    public void CmdBullet(GameObject TrigUnit, int kind)
    {
        GameObject bullet = Instantiate(BulletModel, TrigUnit.transform.position, _spellLib.AimAt.DirctAimQ(TrigUnit)) as GameObject;
        bullet.GetComponent<Excre_class>().ownner = TrigUnit.transform;
        bullet.GetComponent<Excre_class>().LiveTime = 1.5f;
        bullet.GetComponent<Excre_class>().Type = "Shot";
        if (kind == 1)
        {
            bullet.GetComponent<Excre_class>().Name = "Bullet_A";
        }
        else if (kind == 2)
        {
            bullet.GetComponent<Excre_class>().Name = "Bullet_B";
        }
        NetworkServer.Spawn(bullet);
    }
    [Command]
    public void CmdMine(GameObject TrigUnit)
    {
        GameObject mine = Instantiate(Mine, TrigUnit.transform.position, TrigUnit.transform.rotation) as GameObject;
        mine.GetComponent<Excre_class>().ownner = TrigUnit.transform;
        mine.GetComponent<Excre_class>().LiveTime = 5 * 60f;
        mine.GetComponent<Excre_class>().Type = "Trap";
        mine.GetComponent<Excre_class>().Name = "Mine";
        NetworkServer.Spawn(mine);
    }
    [Command]
    public void CmdGranade(GameObject TrigUnit,float val)
    {
        GameObject granade = Instantiate(Granade, TrigUnit.transform.position, TrigUnit.transform.rotation) as GameObject;
        granade.GetComponent<Excre_class>().ownner = TrigUnit.transform;
        granade.GetComponent<Excre_class>().LiveTime = 20;
        granade.GetComponent<Excre_class>().Type = "Shot";
        granade.GetComponent<Excre_class>().Name = "Granade";
        granade.GetComponent<Excre_class>().ExtraVal = val;
        granade.GetComponent<Granade_boom>().Work(val);
        NetworkServer.Spawn(granade);
    }
    [Command]
    public void CmdEffectBindUnit(int EfIndex,GameObject unit)
    {
        efModel = GameObject.Find("System/MyDb").GetComponent<MyDb>().effect[EfIndex];
        GameObject ef = Instantiate(efModel) as GameObject;
        ef.GetComponent<EffectInit>().BindU(unit, EfIndex);
        NetworkServer.Spawn(ef);
    }
    [Command]
    public void CmdEffectBindPos(int EfIndex,Vector3 pos)
    {
        efModel = GameObject.Find("System/MyDb").GetComponent<MyDb>().effect[EfIndex];
        GameObject ef = Instantiate(efModel) as GameObject;
        ef.GetComponent<EffectInit>().BindP(pos, EfIndex);
        NetworkServer.Spawn(ef);
    }
    [Command]
    public void CmdSetPick(GameObject t)
    {
        t.GetComponent<Self_class>().isPick = false;
    }
    //-----------------------------------------------Client
    [ClientRpc]
    public void RpcInjuredDis(GameObject trigunit,GameObject target)
    {
        GameObject locplayer = GameObject.Find("Hero").transform.FindChild("Player").gameObject;
        if (locplayer == trigunit)
        {
            trigunit.GetComponent<Self_class>().s_Canvas.transform.FindChild("RLife").gameObject.SetActive(true);
            trigunit.GetComponent<Self_class>().s_Canvas.transform.FindChild("RLife").GetComponent<Rlife>().setTarget(target);
        }
    }
    //-----------------------------------------------函數
}
