using UnityEngine;

public class movePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("VARIABLES")]
    [SerializeField][Range(1,20)] private float speed;
    [SerializeField][Range(1,20)] private float jumpF;
    private float hmov;
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
        //Movimiento horizontal
        hmov= Input.GetAxis("Horizontal");
        rb.linearVelocity= new Vector2(hmov * speed, rb.linearVelocity.y);
        //Salto
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector2.up * jumpF, ForceMode2D.Impulse);
        }
    }
}