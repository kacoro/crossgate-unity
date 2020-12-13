using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PoolTest : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerInputActions controller;
     private void Awake() {
        controller = new PlayerInputActions();
        controller.GamePlay.Start.performed += ctx => OnStart();
    }

    // Update is called once per framevoid FixedUpdate()
     void FixedUpdate(){
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

         if (gamepad.xButton.wasPressedThisFrame)
        {
            PoolMgr.GetInstance().GetObj("Test/Cube",(obj) =>{});
        }
        if (gamepad.aButton.wasPressedThisFrame)
        {
             PoolMgr.GetInstance().GetObj("Test/Sphere",(obj) =>{});
        }

        Vector2 move = gamepad.leftStick.ReadValue();
        
    }
    
     private void OnEnable() {
        controller.Enable();
    }
    private void OnDisable() {
        controller.Disable();
    }

     public void OnStart()
    {
        Debug.Log("Fire!");
    }
}
