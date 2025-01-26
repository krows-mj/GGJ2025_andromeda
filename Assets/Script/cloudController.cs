using UnityEngine;

public class cloudController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField][Range(0,20)] private float speed;

    private void Awake(){
        rb= GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GAMECONTROLLER.inst.GetPausa()){
            rb.linearVelocity= new Vector2(speed, 0);
        }else{
            rb.linearVelocity= Vector2.zero;
        }
    }

}
