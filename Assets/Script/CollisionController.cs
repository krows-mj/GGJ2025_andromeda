using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private Collider2D col;
    private Rigidbody2D rb;
    [SerializeField] private GAMECONTROLLER.TipeObjects objectTipe;
    [SerializeField] private float imp;
    void Awake(){
        rb= transform.parent.gameObject.GetComponent<Rigidbody2D>();
        col= GetComponent<Collider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActiveCollition();
    }

    // Update is called once per frame
    //void Update(){}

    public void ActiveCollition(){
        col.enabled= true;
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag =="Enemy"){
            GAMECONTROLLER.inst.IReceivedDamage(objectTipe);
            col.enabled= false;
            ImpulseObject();
            Tareas.Nueva(1.5f, ActiveCollition);
        }
    }
    public void ImpulseObject(){
        rb.AddForce(Vector2.up * imp, ForceMode2D.Impulse);
    }
}
