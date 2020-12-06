using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
   public string entrancePassword;
   private void Start() {
       if(PlayerController.Instance){
        if(PlayerController.Instance.scenePassword == entrancePassword){
            PlayerController.Instance.transform.position = transform.position;
            Debug.Log("Enter!");
        }else{
            Debug.Log("Wrong!");
        }
       }
       
   }
}
