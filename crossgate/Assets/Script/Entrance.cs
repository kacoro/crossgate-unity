using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
   public string entrancePassword;
   private void Start() {
       if(PlayerController.instance.scenePassword == entrancePassword){
           PlayerController.instance.transform.position = transform.position;
           Debug.Log("Enter!");
       }else{
           Debug.Log("Wrong!");
       }
   }
}
