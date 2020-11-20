using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]

public class PlayerData
{
    public int level;
    public int health;
    public float[] postion;

    public string sceneName;

    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;
        postion = new float[3];
        postion[0] = player.transform.position.x;
        postion[1] = player.transform.position.y;
        postion[2] = player.transform.position.z;
        sceneName = SceneManager.GetActiveScene().name;
        
    }
}
