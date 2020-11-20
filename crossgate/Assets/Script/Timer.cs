using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
   [SerializeField] private float maxTime;
   [SerializeField] private float currentTime;
   [SerializeField] private Text timeDisplay;

    [SerializeField] private bool isGameOver;

   private void Awake() {
       currentTime = maxTime;
       timeDisplay.text = maxTime.ToString();
   }

   private void Update() {
        if(!isGameOver){
              currentTime -= Time.deltaTime;
            timeDisplay.text = ((int)currentTime).ToString();
        }

        if(currentTime <= 10){
            timeDisplay.color = new Vector4(200,0,0,255);
        }

        if(currentTime <= 0){
             timeDisplay.text = "";
            isGameOver = true;
        }
   }

 
}
