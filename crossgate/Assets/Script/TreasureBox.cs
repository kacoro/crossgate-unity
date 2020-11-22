using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool canOpen;
    private bool isOpend;
    private Animator animator;
     private PlayerInputActions controls;

    public GameObject item;
    public float delayTime = 1f;
    
    private void Awake() {
          controls = new PlayerInputActions();
          controls.GamePlay.Ok.started += ctx => OpenTreasureBox();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpend = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

     private void OnEnable() {
        controls.GamePlay.Enable();
    }

     private void OnDisable() {
         controls.GamePlay.Disable();
     }

    private void OnTriggerEnter2D(Collider2D other) {
         if(other.gameObject.CompareTag("Player")){
            canOpen = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other) {
         if(other.gameObject.CompareTag("Player")){
            canOpen = false;
        }
    }
    
    void GenItem(){
        Instantiate(item,transform.position,Quaternion.identity);
    }

    void OpenTreasureBox(){
        if(canOpen && !isOpend){
            animator.SetTrigger("Opening");
            isOpend = true;
            Invoke("GenItem",delayTime);
        }
    }
}
