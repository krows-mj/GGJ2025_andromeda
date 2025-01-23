using UnityEngine;

public class moveBubble : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("VARIALBES")]
    [SerializeField] private int life;
    [SerializeField][Range(1,20)] private float impulseF;
    
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
        if(Input.GetKeyDown(KeyCode.O)){
            rb.AddForce(Vector2.up * impulseF, ForceMode2D.Impulse);
        }
    }
    [ContextMenu("ZERO")]public void ZERO(){transform.position= Vector3.zero;}
    //Getters and Setters
    public void SetLife(int n){life=n;}
    public int GetLife(){return life;}
}
