using UnityEngine;

public class uiController : MonoBehaviour
{
    public GameObject MenuP;
    public GameObject PanelMenu;
    public GameObject WinGame;
    public GameObject GameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuP.SetActive(true);
        PanelMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
