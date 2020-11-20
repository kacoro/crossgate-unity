using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; //keep in the different scene

   private Rigidbody2D rb;
   public float moveH,moveV; //keep in the different scene
   [SerializeField] private float moveSpeed = 4.0f;

    public string scenePassword;

    private PlayerInputActions controls;
    private Vector2 move;

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
    }

}
