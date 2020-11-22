using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class NPCTigger : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;

    public string text;

    public bool isActive = false; 

    private bool isTriggerEnter;

    private PlayerInputActions controls;

    private void Awake() {
            controls = new PlayerInputActions();
          controls.GamePlay.Ok.started += ctx => ShowDialog();
    }

    private void OnEnable() {
        controls.GamePlay.Enable();
    }

     private void OnDisable() {
         controls.GamePlay.Disable();
     }

    private void OnTriggerEnter2D(Collider2D other) {
        // if(other.gameObject.CompareTag("Player")&&other.GetType().ToString()=="CircleCollider2D"){
        //     Debug.Log(other.GetType().ToString());
        // }
        if(other.gameObject.CompareTag("Player")){
            isTriggerEnter = true;
        }
    }

    private void ShowDialog(){
        if(isTriggerEnter){
           dialogText.text = text;
           dialogBox.SetActive(isActive=!isActive);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
         if(other.gameObject.CompareTag("Player")){
             dialogBox.SetActive(false);
            isTriggerEnter = false;
        }
    }
}
