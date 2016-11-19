using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {
    private enum Uimod
    {
        Author,
        Loading,
        Menu,
        Start,
    }
    private Uimod Nui;
    private Transform Author, mMenu, Loading,StartM,Proc;
    private AsyncOperation Asy;
    void Awake()
    {
        Author = this.transform.FindChild("Author");
        Loading = this.transform.FindChild("Loading");
        mMenu = this.transform.FindChild("Menu");
        StartM = this.transform.FindChild("StartM");
        Proc = this.transform.FindChild("StartM").FindChild("HT").FindChild("Process");
        Nui = Uimod.Loading;
    }
    void Start()
    {
        StartCoroutine(WaitWork());
    }
    void Update()
    {
        if (Nui == Uimod.Loading)
        {
            mMenu.gameObject.SetActive(false);
            Author.gameObject.SetActive(false);
            Loading.gameObject.SetActive(true);
            StartM.gameObject.SetActive(false);
        }
        else if (Nui == Uimod.Menu)
        {
            Loading.gameObject.SetActive(false);
            Author.gameObject.SetActive(false);
            mMenu.gameObject.SetActive(true);
            StartM.gameObject.SetActive(false);
        }
        else if (Nui == Uimod.Author)
        {
            Loading.gameObject.SetActive(false);
            Author.gameObject.SetActive(true);
            mMenu.gameObject.SetActive(false);
            StartM.gameObject.SetActive(false);
        }
        else if (Nui == Uimod.Start)
        {
            Loading.gameObject.SetActive(false);
            Author.gameObject.SetActive(false);
            mMenu.gameObject.SetActive(false);
            StartM.gameObject.SetActive(true);
            Proc.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0,Asy.progress*Proc.parent.GetComponent<RectTransform>().rect.size.x);
        }
    }
    public void NuiAu()
    {
        Nui = Uimod.Author;
    }
    public void NuiMu()
    {
        Nui = Uimod.Menu;
    }
    public void NuiSu()
    {
        StartCoroutine(LoadS());
        Nui = Uimod.Start;
    }
    IEnumerator WaitWork()
    {
        yield return new WaitForSeconds(5f);
        Nui = Uimod.Menu;
    }
    IEnumerator LoadS()
    {
        yield return Asy = SceneManager.LoadSceneAsync(1);
    }
}
