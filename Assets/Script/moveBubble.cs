using UnityEngine;

public class moveBubble : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("VARIALBES")]
    [SerializeField] private int life;
    [SerializeField][Range(1,20)] private float impulseF;
    [SerializeField][Range(0f,1f)] private float speedPercentage;
    private Vector2 inCloudSpeed;
    
    private void Awake(){
        rb= GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if(!GAMECONTROLLER.inst.GetPausa()){
            if(Input.GetKeyDown(KeyCode.O)){
                rb.AddForce(Vector2.up * impulseF, ForceMode2D.Impulse);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag =="Cloud"){
            inCloudSpeed.x= rb.linearVelocity.x;
            inCloudSpeed.y= rb.linearVelocity.y * speedPercentage;
        }
    }
    public void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.tag =="Cloud"){
            rb.linearVelocity= new Vector2(rb.linearVelocity.x, inCloudSpeed.y);
        }
    }
    //public void OnTriggerExit2D(Collider2D col){}
    [ContextMenu("ZERO")]public void ZERO(){transform.position= Vector3.zero;}
    //Getters and Setters
    public void SetLife(int n){life=n;}
    public int GetLife(){return life;}
}
