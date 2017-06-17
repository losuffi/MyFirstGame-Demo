using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class BoardCast : NetworkDiscovery {
    public static BoardCast Bc;
    public delegate void GetMsgByHandle(string Ip, string data);
    public event GetMsgByHandle GetMsg;
    private string DATA;
    void Awake()
    {
        Bc = this;
    }
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (GetMsg != null)
        {
            GetMsg(fromAddress, data);
        }
    }
    public void AsServe()
    {
        StopBroadcast();
        Initialize();
        broadcastData = DATA;
        StartAsServer();
    }
    public void AsClient()
    {
        Initialize();
        StartAsClient();
    }
    public void Setdata(string _data)
    {
        if (!isServer)
            return;
        StartCoroutine(WaitSetdata(_data));
    }
    public void Initdata(string _data)
    {
        DATA = _data;
    }
    IEnumerator WaitSetdata(string _data)
    {
        while (!isServer)
            yield return 0;
        string temp = broadcastData;
        DATA = temp.Split('#')[0] + "#" + _data;
        StopBroadcast();
        Initialize();
        broadcastData = DATA;
        StartAsServer();
        StopCoroutine(WaitSetdata(_data));
    }
}
