using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; //keep in the different scene

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
        animator = GetComponent<CharacterAnimator>();
        controls.GamePlay.Ok.started += ctx => Attack();
        isPlaying = true;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
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


    public void HandleUpdate()
    {

        moveH = move.x * moveSpeed;
        moveV = move.y * moveSpeed;
        rb.velocity = new Vector2(moveH, moveV);
        Vector2 direction = new Vector2(moveH, moveV);
        animator.Direction = direction;
        if (direction.magnitude < 0.01)
        {
            animator.IsMoving = false;
        }
        else
        {
            animator.IsMoving = true;
        }
        // animator.SetDirection(direction);
        // FindObjectOfType<PlayerAnimation>().SetDirection(direction);
        if (move != Vector2.zero)
        {
            CheckForEncounters();
        }
    }

    public void Attack()
    {
        animator.IsAttack = true;
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
        if (Physics2D.OverlapCircle(transform.position, 0.2f, enCouterLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 1001) <= 10)
            {
                rb.velocity = Vector3.zero;
                Vector2 direction = Vector2.zero;
                FindObjectOfType<PlayerAnimation>().SetDirection(direction);
                Debug.Log("Encountered a wild " + Time.deltaTime);
                OnEncountered();
            }
        }

    }
}
