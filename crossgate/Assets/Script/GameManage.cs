using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    private GameObject[] players;
    private GameObject[] enemys;
   
    private List<GameObject> player;
    private List<GameObject> enemy;
     private List<GameObject> team;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        player = new List<GameObject>();
        enemy = new List<GameObject>();
        foreach(var o in players)
            player.Add(o);

        foreach(var o in enemys)
            enemy.Add(o);

        team = player;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Announce(GameObject go){
        team[index].GetComponent<ObjectControl>().GetMove = true;
    }
}
