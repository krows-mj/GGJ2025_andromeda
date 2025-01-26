using UnityEngine;
using System.Collections.Generic;

public class BossSad : MonoBehaviour
{
    //private Rigidbody2D rb;
    private SpriteRenderer srBody, srHands;
    private Collider2D col;
    public enum State {idle, move, attack, die}
    [Header("VARIABLES")]
    
    public State BossState;
    [SerializeField] private int life;
    [SerializeField][Range (0f,10f)] private float speed;
    [SerializeField] private Transform target;
    [Header("Puntos de movimiento")]
    public List<Transform> ReferencePoints= new List<Transform>();
    private int currentPoint;

    private void Awake(){
        //rb= GetComponent<Rigidbody2D>();
        srBody= transform.GetChild(0).GetComponent<SpriteRenderer>();
        srHands= transform.GetChild(1).GetComponent<SpriteRenderer>();
        col= GetComponent<Collider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPoint= 0;
        ChangeTimeState();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GAMECONTROLLER.inst.GetPausa()){
        VistaPlayer();
        switch(BossState){
            case State.idle:
                break;
            case State.move:
                if(Vector3.Distance(transform.position, ReferencePoints[currentPoint].position) >= 1f){
                    transform.position = 
                    Vector3.MoveTowards(transform.position, ReferencePoints[currentPoint].position, speed * Time.deltaTime);
                }else{
                    //Debug.Log(Vector3.Distance(transform.position, ReferencePoints[currentPoint].position));
                    NextPoint(); 
                    BossState= State.idle;
                    ChangeTimeState();
                } 
                break;
            default:
                BossState= State.idle;
                break;
        }
        }
    }
    public void ActiveCollition(){
        col.enabled= true;
    }
    public void VistaPlayer(){
        if(transform.position.x - 1.5f > target.position.x){
            srBody.flipX= false;
            srHands.flipX= false;
        }
        if(transform.position.x + 1.5f < target.position.x){
            srBody.flipX= true;
            srHands.flipX= true;
        }
    }
    public void ChangeTimeState(){
        Tareas.Nueva(Random.Range(2f,4f), ChangeState);
    }

    public void ChangeState(){
        BossState= State.move;
    }
    private void NextPoint(){
        currentPoint= (ReferencePoints.Count <= currentPoint+1)? 0: currentPoint+1;  
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag =="Attack"){
            col.enabled= false;
            GAMECONTROLLER.inst.WinGame();
            Tareas.Nueva(1f, ActiveCollition);
        }
    }

    //Getters and Setters
    public void SetLife(int n){life=n;}
    public int GetLife(){return life;}
}
