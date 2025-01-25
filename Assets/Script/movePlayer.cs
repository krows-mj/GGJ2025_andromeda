using UnityEngine;

public class movePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [Header("VARIABLES")]
    [SerializeField] private int life;
    [SerializeField][Range(1,10)] private float speed;
    [SerializeField][Range(1,10)] private float jumpF;
    private bool isGround;
    private float hmov;
    private void Awake(){
        rb= GetComponent<Rigidbody2D>();
        sr= transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life=3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movimiento horizontal
        hmov= Input.GetAxis("Horizontal");
        if(hmov > 0) sr.flipX=true;
        if(hmov < 0) sr.flipX=false;
        rb.linearVelocity= new Vector2(hmov * speed, rb.linearVelocity.y);
        //Salto
        if(Input.GetKeyDown(KeyCode.Space) && isGround){
            rb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
        }
    }
    public void movAnim(){}
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag =="Ground"){
            isGround= true;
        }
    }
    public void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag =="Ground"){
            isGround= false;
        }
    }
    //Getters and Setters
    public void SetLife(int n){life=n;}
    public int GetLife(){return life;}
}