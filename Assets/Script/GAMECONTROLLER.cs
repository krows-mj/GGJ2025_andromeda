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
        lifeBoss= 5;
        lifePlayer= 3;
        lifeBubble= 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame(){
        bossScript.SetLife(lifeBoss);
        playerScript.SetLife(lifePlayer);
        bubbleScript.SetLife(lifeBubble);
        
    }
    public void IReceivedDamage(TipeObjects obj){
        if(TipeObjects.player == obj) lifePlayer--;
        if(TipeObjects.bubble == obj) lifeBubble--;
        GameOver();
    }
    public void WinGame(){
        lifeBoss--;
        if(lifeBoss <= 0){
            Debug.Log("Fin de la partida");
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
    public void GameOver(){
        if(lifePlayer <= 0 || lifeBubble <= 0){
            Debug.Log("Fin partida");
        }
    }
    //Getters and Setters
    public bool GetPausa(){return pausa;}
    public void SetPausa(bool b){pausa=b;}
}
