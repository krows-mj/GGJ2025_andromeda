using UnityEngine;
using System.Collections.Generic;

public class BossSad : MonoBehaviour
{
    //private Rigidbody2D rb;
    public enum State {idle, move, attack, die}
    [Header("VARIABLES")]
    
    public State BossState;
    [SerializeField] private int life;
    [SerializeField][Range (0f,10f)] private float speed;
    [Header("Puntos de movimiento")]
    public List<Transform> ReferencePoints= new List<Transform>();
    private int currentPoint;

    private void Awake(){
        //rb= GetComponent<Rigidbody2D>();
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
        switch(BossState){
            case State.idle:
                break;
            case State.move:
                if(Vector3.Distance(transform.position, ReferencePoints[currentPoint].position) >= 1f){
                    transform.position = 
                    Vector3.MoveTowards(transform.position, ReferencePoints[currentPoint].position, speed);
                }else{
                    Debug.Log(Vector3.Distance(transform.position, ReferencePoints[currentPoint].position));
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

    public void ChangeTimeState(){
        Tareas.Nueva(Random.Range(2f,4f), ChangeState);
    }

    public void ChangeState(){
        BossState= State.move;
    }
    private void NextPoint(){
        currentPoint= (ReferencePoints.Count <= currentPoint+1)? 0: currentPoint+1;  
    }

    //Getters and Setters
    public void SetLife(int n){life=n;}
    public int GetLife(){return life;}
}
