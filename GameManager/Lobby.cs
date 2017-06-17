using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Lobby : NetworkLobbyManager {
    private int num;
    public override void OnLobbyClientEnter()
    {
        base.OnLobbyClientEnter();
        num++;
        GameObject.Find("BoadCast").GetComponent<BoardCast>().Setdata(num + "/" + maxPlayers);
    }
    public void Set(int numplayer)
    {
        maxPlayers = numplayer;
        minPlayers = numplayer;
    }
    public void SetClientIP(string ip)
    {
        NetworkManager.singleton.networkAddress = ip;
    }
    IEnumerator Check()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckReadyToBegin();
        }
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        num = 0;
        StartCoroutine(Check());
    }
    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        if (sceneName == "demo1")
        {
            StopCoroutine(Check());
        }
    }
    public override void OnLobbyClientExit()
    {
        base.OnLobbyClientExit();
        num--;
    }
}
