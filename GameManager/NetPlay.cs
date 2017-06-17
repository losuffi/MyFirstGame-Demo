using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class NetPlay : NetworkManager {
    public void CreateGame()
    {
        StartHost();
    }
    public void JoinGame(string addr)
    {
        networkAddress = addr;
        StartClient();
    }
    void ClientHq()
    {
        Debug.Log(NetworkClient.allClients);
    }
}
