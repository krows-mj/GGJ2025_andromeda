using UnityEngine;

public class movePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    [Header("VARIABLES")]
    [SerializeField] private int life;
    [SerializeField][Range(1,10)] private float speed;
    [SerializeField][Range(1,10)] private float jumpF;
    private GameObject clawAttack;
    private SpriteRenderer spClaw;
    private bool isGround;
    private float hmov;
    private void Awake(){
        rb= GetComponent<Rigidbody2D>();
        sr= transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim= transform.GetChild(0).GetComponent<Animator>();
        clawAttack= transform.GetChild(2).gameObject;
        spClaw= clawAttack.GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life=3;
        offClaw();
    }

    // Update is called once per frame
    void Update(){
        if(!GAMECONTROLLER.inst.GetPausa()){
            anim.SetFloat("walk",hmov*hmov);
            anim.SetBool("jump", !isGround);
            if(Input.GetKeyDown(KeyCode.A)){
                if(clawAttack.transform.localPosition.x < 0){
                    movAtt(1);
                }else{movAtt(-1);}
            }
            if(Input.GetKeyDown(KeyCode.D)){
                if(clawAttack.transform.localPosition.x > 0){
                    movAtt(1);
                }else{movAtt(-1);}
            }
            //Ataque
            if((Input.GetAxis("Fire1")) > 0){
                clawAttack.SetActive(true);
                Tareas.Nueva(0.3f, offClaw);
            }
        }

    }
    void FixedUpdate()
    {
        if(!GAMECONTROLLER.inst.GetPausa()){
            //Movimiento horizontal
            hmov= Input.GetAxis("Horizontal");
            
            if(hmov > 0){ sr.flipX=false; spClaw.flipX=false; }
            if(hmov < 0){ sr.flipX=true; spClaw.flipX=true;} 
            rb.linearVelocity= new Vector2(hmov * speed, rb.linearVelocity.y);
            //Salto
            
            if(Input.GetKeyDown(KeyCode.W) && isGround){

                rb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
            }
        }
    }
    public void offClaw(){
        clawAttack.SetActive(false);
    }
    public void movAtt(int n){
        clawAttack.transform.localPosition= new Vector3(
            clawAttack.transform.localPosition.x *n, 
            clawAttack.transform.localPosition.y, 
            clawAttack.transform.localPosition.z); 

    }
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