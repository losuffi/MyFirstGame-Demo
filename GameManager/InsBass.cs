using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class InsBass : NetworkBehaviour {
    [SyncVar]
    public Vector3 pos;
    [SyncVar]
    public Quaternion qua;
    [SyncVar]
    public string Mname="None";
    [SyncVar]
    public GameObject obj;
    public bool isInit = false;
    private bool isSet = false;
    void Update()
    {
        if (!isSet&&isServer&&isInit && obj != null)
        {
            GameObject weaponInit= obj.transform.Find("RigPelvis/RigSpine1/RigSpine2/RigRibcage/RigRArm1/RigRArm2/RigRArmPalm/Dummy-RHandWeapon/Sword").gameObject;
            try
            {
                GameObject weaponEqui = obj.transform.Find("RigPelvis/RigSpine1/RigSpine2/RigRibcage/RigRArm1/RigRArm2/RigRArmPalm/Dummy-RHandWeapon/Equi").gameObject;
                obj.GetComponent<UnitSyncCmd>().CmdDes(weaponEqui);
            }
            catch { }
            pos = weaponInit.transform.position;
            qua = weaponInit.transform.rotation * new Quaternion(-1, 0, 0, 0) * new Quaternion(0, 1, 0, 1);
            transform.position = pos;
            transform.rotation = qua;
            transform.parent = weaponInit.transform.parent;
            transform.name = "Equi";
            isSet = true;
        }
        else if (!isSet && obj != null) 
        {
            GameObject weaponInit = obj.transform.Find("RigPelvis/RigSpine1/RigSpine2/RigRibcage/RigRArm1/RigRArm2/RigRArmPalm/Dummy-RHandWeapon/Sword").gameObject;
            pos = weaponInit.transform.position;
            qua = weaponInit.transform.rotation * new Quaternion(-1, 0, 0, 0) * new Quaternion(0, 1, 0, 1);
            transform.position = pos;
            transform.rotation = qua;
            transform.parent = weaponInit.transform.parent;
            transform.name = "Equi";
            isSet = true;
        }
    }
}
