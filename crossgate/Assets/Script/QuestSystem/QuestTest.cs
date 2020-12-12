using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
public class QuestTest : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
        CollectionObjective qb = new CollectionObjective("Gather",10,item,"Gather 10 meat!",false);
        Debug.Log(qb.ToString());
        qb.UpdateCurrentAmount(10);
        Debug.Log(qb.ToString());
        Debug.Log(qb.IsComplete);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
