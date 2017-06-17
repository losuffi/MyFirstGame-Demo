using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour {
    private Transform player;
    private Transform RomotePlayer;
    public static GameManager _ins;
    public delegate void _insplayer(GameObject obj);
    public event _insplayer Insplayer;
    void Awake()
    {
        _ins = this;
    }
    public void CreateGame()
    {
        this.transform.GetComponent<NetPlay>().CreateGame();
    }
    public void JoinGame(string Adr)
    {
        this.transform.GetComponent<NetPlay>().JoinGame(Adr);
    }
    void Init()
    {
        GameObject Hero = GameObject.Find("Hero");
        GameObject Camera = GameObject.Find("PlayerCamera");
        Transform playercanvas = GameObject.Find("Canvas").transform;
        player.parent = Hero.transform;
        playercanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        player.GetComponent<Self_class>().BindCanvas(playercanvas);
        Camera.GetComponent<PlayerCamera>().BindPlayer(player);
        playercanvas.transform.GetComponent<Canvas_Init>().BindPlayer(player);
    }
    Vector3 Rndpos()
    {
        float p_x, p_y, p_z;
        p_x = Random.Range(105, 425);
        p_z = Random.Range(65, 425);
        p_y = Terrain.activeTerrain.SampleHeight(new Vector3(p_x, 1, p_z));
        return new Vector3(p_x, p_y, p_z);
    }
    public void reg()
    {
        player = GameObject.Find("Player").transform;
        player.position = Rndpos();
        if (Insplayer != null)
            Insplayer(player.gameObject);
        Init();
        StartCoroutine(MapInit());
    }
    public void clientReg()
    {
        player = GameObject.Find("Player").transform;
        player.position = Rndpos();
        Init();
    }
    public void RomoteReg()
    {
        RomotePlayer = GameObject.Find("RomotePlayer").transform;
        RomotePlayer.parent = GameObject.Find("Hero").transform;
        if (Insplayer != null)
            Insplayer(RomotePlayer.gameObject);
    }
    IEnumerator MapInit()
    {
        yield return new WaitForSeconds(0.1f);
        EnvironmentCreate._ins.CreateEnv();
        //transform.parent.FindChild("SyncCenter").GetComponent<SyncCenter>().SyncCenterInit();
    }
}
