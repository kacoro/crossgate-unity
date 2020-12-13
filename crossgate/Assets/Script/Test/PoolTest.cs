using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PoolTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per framevoid FixedUpdate()
     void FixedUpdate(){
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

         if (gamepad.xButton.wasPressedThisFrame)
        {
            PoolMgr.GetInstatnce().GetObj("Test/Cube");
        }
        if (gamepad.aButton.wasPressedThisFrame)
        {
             PoolMgr.GetInstatnce().GetObj("Test/Sphere");
        }

        Vector2 move = gamepad.leftStick.ReadValue();
        
    }
    

     public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }
}
