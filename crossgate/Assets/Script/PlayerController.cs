using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance{ get; private set; } //keep in the different scene

    private Rigidbody2D rb;
    public float moveH, moveV; //keep in the different scene
    [SerializeField] private float moveSpeed = 4.0f;

    public string scenePassword;

    public LayerMask enCouterLayer;

    private PlayerInputActions controls;
    private Vector2 move;

    private bool isMoving = true;

    private bool isEncouter;

    public event Action OnEncountered;

    private bool isPlaying; // 判断是否处于运行状态

    private CharacterAnimator animator;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
        // controls.GamePlay.MouseDown.started += ctx => MouseDown();
        animator = GetComponent<CharacterAnimator>();
        // controls.GamePlay.Ok.started += ctx => StartCoroutine(Attack());
        isPlaying = true;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    public void MouseDown(){
        Ray ray=Camera.main.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());

        // Vector3 targetPos = ray.origin;
        // StartCoroutine(MovePlayer(targetPos));
    }

    public void HandleUpdate()
    {

        moveH = move.x * moveSpeed;
        moveV = move.y * moveSpeed;
        Vector2 direction = new Vector2(moveH, moveV);
        Vector2 targetPos =  rb.velocity;
        animator.Direction = direction;
       
        rb.velocity = new Vector2(moveH, moveV);
        if ((targetPos - rb.velocity).sqrMagnitude> Mathf.Epsilon){
           animator.IsMoving = false;
           CheckForEncounters();
             
        }else{
            animator.IsMoving = true;
        }
        
        // animator.SetDirection(direction);
        // FindObjectOfType<PlayerAnimation>().SetDirection(direction);
       
    }

   
    IEnumerator MovePlayer(Vector3 targetPos)
    { //固定移动

        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, enCouterLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                rb.velocity = Vector3.zero;
                Vector2 direction = Vector2.zero;
                Debug.Log("Encountered a wild " + Time.deltaTime);
                OnEncountered();
            }
        }

    }
}
