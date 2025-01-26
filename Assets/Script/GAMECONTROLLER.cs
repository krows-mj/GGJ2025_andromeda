using UnityEngine;
using System.Collections.Generic;

public class GAMECONTROLLER : MonoBehaviour
{
    private BossSad bossScript;
    private movePlayer playerScript;
    private moveBubble bubbleScript;
    public enum TipeObjects {player, bubble, enemy}
    [Header("Variables en Juego")]
    public static GAMECONTROLLER inst;
    [SerializeField] private int lifePlayer, lifeBubble, lifeBoss;
    [SerializeField] private bool pausa;
    [Header("Spawn")]
    public List<GameObject> CloudList= new List<GameObject>(); 

    private void Awake(){
        if(GAMECONTROLLER.inst == null){
            GAMECONTROLLER.inst= this;
        }else{ Destroy(gameObject);}
        bossScript= GameObject.Find("BossSandness").gameObject.GetComponent<BossSad>();
        playerScript= GameObject.Find("Player").gameObject.GetComponent<movePlayer>();
        bubbleScript= GameObject.Find("Bubble").gameObject.GetComponent<moveBubble>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetPausa(true);
        lifeBoss= 1;
        lifePlayer= 3;
        lifeBubble= 3;
        bossScript.gameObject.SetActive(false);
        playerScript.gameObject.SetActive(false);
        bubbleScript.gameObject.SetActive(false);
        for(int i=0; i<CloudList.Count; i++){
            CloudList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(){
        lifeBoss= 1;
        lifePlayer= 3;
        lifeBubble= 3;
        bossScript.SetLife(lifeBoss);
        playerScript.SetLife(lifePlayer);
        bubbleScript.SetLife(lifeBubble);
        SetPausa(false);
        playerScript.gameObject.SetActive(true);
        bubbleScript.gameObject.SetActive(true);
        Tareas.Nueva(3f, ActiveBoss);
        Tareas.Nueva(1f, SpawnCloud);
    }
    public void IReceivedDamage(TipeObjects obj){
        if(TipeObjects.player == obj) lifePlayer--;
        if(TipeObjects.bubble == obj) lifeBubble--;
        GameOver();
    }
    public void WinGame(){
        lifeBoss--;
        if(lifeBoss <= 0){
            uiController.inst.PanelGameWin(true);
            uiController.inst.PanelMenus(true);
            bossScript.gameObject.SetActive(false);
            playerScript.gameObject.SetActive(false);
            bubbleScript.gameObject.SetActive(false);
            //Debug.Log("Fin de la partida");
        }
    }
    public void SpawnCloud(){
        for(int i=0; i<CloudList.Count; i++){
            if(CloudList[i].activeSelf){
                CloudList[i].transform.position= new Vector3(-9f,Random.Range(-2f, 4f),0f);
                CloudList[i].SetActive(true);
                break;
            }
        }
        Tareas.Nueva(Random.Range(3f, 6f), SpawnCloud);
    }
    public void ActiveBoss(){
        bossScript.gameObject.SetActive(true);
    }
    public void GameOver(){
        if(lifePlayer <= 0 || lifeBubble <= 0){
            uiController.inst.PanelGameOver(true);
            uiController.inst.PanelMenus(true);
            bossScript.gameObject.SetActive(false);
            playerScript.gameObject.SetActive(false);
            bubbleScript.gameObject.SetActive(false);
            Debug.Log("Fin partida");
        }
    }
    //Getters and Setters
    public bool GetPausa(){return pausa;}
    public void SetPausa(bool b){pausa=b;}
}
