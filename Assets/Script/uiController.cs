using UnityEngine;
using UnityEngine.SceneManagement;

public class uiController : MonoBehaviour
{
    public static uiController inst;
    public GameObject MenuP;
    public GameObject PanelMenu;
    public GameObject WinGame;
    public GameObject GameOver;
    void Awake(){
        if(uiController.inst == null){
            uiController.inst= this;
        }else{ Destroy(gameObject);}
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PanelGameMenu(true);
        PanelMenus(true);
        PanelGameWin(false);
        PanelGameOver(false);
    }

    // Update is called once per frame
    //void Update(){}

    public void PanelGameOver(bool b){
        GameOver.SetActive(b);
    }
    public void PanelGameWin(bool b){
        WinGame.SetActive(b);
    }
    public void PanelGameMenu(bool b){
        PanelMenu.SetActive(b);
    }
    public void PanelMenus(bool b){ //imagen del fondo
        MenuP.SetActive(b);
    }
}
