using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class LobbyPlayer : NetworkLobbyPlayer {
    public override void OnClientEnterLobby()
    {
        this.name = "LP" + slot;
        this.gameObject.GetComponent<Lobbywait>().Ks();
        readyToBegin = true;
    }
}
