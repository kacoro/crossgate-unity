using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour
{
    // Start is called before the first frame update
     private void OnEnable() { //激活时
        Invoke("Push",1);
    }

    // Update is called once per frame
    void Push(){
        Debug.Log(this.gameObject.name);
        PoolMgr.GetInstance().PushObj(this.gameObject.name,this.gameObject);
    }
}
