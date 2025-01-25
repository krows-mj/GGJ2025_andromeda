using UnityEngine;

public class GAMECONTROLLER : MonoBehaviour
{
    private BossSad bossScript;
    private movePlayer playerScript;
    private moveBubble bubbleScript;
    [Header("Variables en Juego")]
    [SerializeField] private int lifePlayer, lifeBubble, lifeBoss;
    [SerializeField] private bool pausa;

    private void Awake(){
        bossScript= GameObject.Find("BossSandness").gameObject.GetComponent<BossSad>();
        playerScript= GameObject.Find("Player").gameObject.GetComponent<movePlayer>();
        bubbleScript= GameObject.Find("Circle").gameObject.GetComponent<moveBubble>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
