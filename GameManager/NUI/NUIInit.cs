using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class NUIInit : MonoBehaviour {
    public static NUIInit NI;
    public GameObject LanMenu, CrtMenu,Menu;
    public GameObject ListMode;
    private Hashtable RList = new Hashtable();
    private GameObject otherContent;
    private string SetConectIp;
    void Awake()
    {
        NI = this;
        GameObject.Find("NUI").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        LanMenu = transform.Find("LANMenu").gameObject;
        CrtMenu = transform.Find("CrtMenu").gameObject;
        Menu = transform.Find("Menu").gameObject;
        SetConectIp = "0";
    }
    void Start()
    {
        LanMenu.SetActive(false);
        CrtMenu.SetActive(false);
    }
    public void Click(string bot)
    {
        switch (bot)
        {
            case "LANGame":
                ViewLanGame();
                break;
            case "Bac":
                BackMenu();
                break;
            case "Crt":
                Dis(CrtMenu);
                break;
            case "Confi":
                CrtLanGame();
                break;
            case "Joi":
                JoinGame();
                break;
            default:
                break;
        }
    }
    void Dis(GameObject obj)
    {
        Menu.SetActive(false);
        LanMenu.SetActive(false);
        CrtMenu.SetActive(false);
        obj.SetActive(true);
    }
    void CrtLanGame()
    {
        string Name = CrtMenu.transform.Find("InName/Text").GetComponent<Text>().text;
        string temp= CrtMenu.transform.Find("InCount/Text").GetComponent<Text>().text;
        int count = 0;
        try
        {
            count = int.Parse(temp);
        }
        catch
        {
            CrtMenu.transform.Find("Notice").GetComponent<Text>().text = "玩家数量设置不正确";
        }
        if (Name == "")
        {
            CrtMenu.transform.Find("Notice").GetComponent<Text>().text = "名称未设置";
        }
        else if(count <= 0)
        {
            CrtMenu.transform.Find("Notice").GetComponent<Text>().text = "玩家数量设置不正确";
        }
        else
        {
            GameObject.Find("BoadCast").GetComponent<BoardCast>().Initdata(Name + "#" + count);
            GameObject.Find("BoadCast").GetComponent<BoardCast>().AsServe();
            GameObject.Find("LobbyManager").GetComponent<Lobby>().Set(count);
            GameObject.Find("LobbyManager").GetComponent<Lobby>().StartHost();

        }
    }
    void ViewLanGame()
    {
        RList.Clear();
        Dis(LanMenu);
        int i = LanMenu.transform.Find("List/Main").childCount;
        for(int j = 0; j < i; j++)
        {
            Destroy(LanMenu.transform.Find("List/Main").GetChild(j).gameObject);
        }
        GameObject.Find("BoadCast").GetComponent<BoardCast>().AsClient();
        GameObject.Find("BoadCast").GetComponent<BoardCast>().GetMsg += AddGame;
    }
    void BackMenu()
    {
        GameObject.Find("BoadCast").GetComponent<BoardCast>().GetMsg -= AddGame;
        GameObject.Find("BoadCast").GetComponent<BoardCast>().StopBroadcast();
        Dis(Menu);
    }
    void AddGame(string ip,string data)
    {
        if (RList.Contains(ip))
        {
            string temp = (string)RList[ip];
            if (temp == data)
                return;
            else
            {
                RList.Remove(ip);
            }
        }
        GameObject addlist = Instantiate(ListMode, LanMenu.transform.Find("List/Main")) as GameObject;
        addlist.name = ip;
        addlist.transform.Find("RName").GetComponent<Text>().text = data.Split('#')[0];
        addlist.transform.Find("RCount").GetComponent<Text>().text = data.Split('#')[1];
        RList.Add(ip, data);
        addlist.GetComponent<ConCurrent>().get += CurrentGame;
    }
    void CurrentGame(string ip,GameObject o)
    {
        if(otherContent!=null)
            otherContent.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        o.GetComponent<Image>().color = new Color(1, 0.2f, 0.2f, 0.5f);
        SetConectIp = ip;
        otherContent = o;
    }
    void JoinGame()
    {
        if (SetConectIp == "0")
            return;
        GameObject.Find("LobbyManager").GetComponent<Lobby>().SetClientIP(SetConectIp);
        GameObject.Find("LobbyManager").GetComponent<Lobby>().StartClient();
    }
}
