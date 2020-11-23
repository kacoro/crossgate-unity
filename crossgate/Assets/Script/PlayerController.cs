using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; //keep in the different scene

   private Rigidbody2D rb;
   public float moveH,moveV; //keep in the different scene
   [SerializeField] private float moveSpeed = 4.0f;

    public string scenePassword;

    public LayerMask enCouterLayer;

    private PlayerInputActions controls;
    private Vector2 move;

    private bool isMoving = true;

    private bool isEncouter;

    private void Awake() {
        controls = new PlayerInputActions();
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled  += ctx => move = Vector2.zero;

        // controls.GamePlay.Ok.started += Ok();

        if(instance == null) {
            instance = this;
        }else{
            if(instance !=this){
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        controls.GamePlay.Enable();
    }

     private void OnDisable() {
         controls.GamePlay.Disable();
     }

   private void Start() {
       rb = GetComponent<Rigidbody2D>();
   }

    private void FixedUpdate()
    {
        //moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
        //moveV = Input.GetAxisRaw("Vertical") * moveSpeed;
         
         moveH = move.x * moveSpeed;
                moveV = move.y * moveSpeed;
                rb.velocity = new Vector2(moveH, moveV);
                Vector2 direction = new Vector2(moveH, moveV);
                FindObjectOfType<PlayerAnimation>().SetDirection(direction);
            // CheckForEncounters();
          if(move!=Vector2.zero){
                //Debug.Log(move!=Vector2.zero);
                StartCoroutine(CheckForEncounters());
          }
         
      
    }

    IEnumerator MovePlayer(Vector3 targetPos){ //固定移动

        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon){
            transform.position = Vector3.MoveTowards(transform.position,targetPos,moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    IEnumerator CheckForEncounters(){
        if(Physics2D.OverlapCircle(transform.position,0.2f,enCouterLayer)!=null){
            if(isEncouter){
                isEncouter = false;
                 yield return new WaitForSeconds(6.0f);
            }else{
                if(Random.Range(1,1010)<=10){
                    Debug.Log("Encountered a wild "+Time.deltaTime);
                    isEncouter = true;
                yield return new WaitForSeconds(3.0f);
                }
            }
            
        }
         
    }
}
