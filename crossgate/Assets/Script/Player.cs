using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public int level = 3;
   public int health = 4;
   public string sceneName = "SampleScene";

   public void SavePlayer(){
       Debug.Log("save");
       SaveSystem.SavePlayer(this);
   }

   public void LoadPlayer(){
        Debug.Log("load");
       PlayerData data = SaveSystem.LoadPlayer();

       level = data.level;
       health = data.health;
       sceneName = data.sceneName;  
       Vector3 position;
        FindObjectOfType<SceneFader>().FadeTo(sceneName);
       position.x = data.postion[0];
       position.y = data.postion[1];
       position.z = data.postion[2];
       transform.position = position;
   }
}
