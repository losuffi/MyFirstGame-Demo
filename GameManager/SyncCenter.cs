using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class SyncCenter : NetworkBehaviour
{
    public void SyncCenterInit()
    {
        NetworkServer.Spawn(this.gameObject);
    }
}

