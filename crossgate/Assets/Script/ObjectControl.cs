using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : BaseObj
{
    private Vector3 primitivePos;
    private Vector3 primitiveRot;
    private Vector3 tempPos;
    // Start is called before the first frame update
    private bool canMove;

    private Vector2 targetPos;
     private float distantce;
    
    public bool GetMove{
        set {canMove = value;}
        get {return canMove;}
    }

    void Start()
    {
        distantce = 1;
        canMove = true;
        primitivePos = transform.position;
        // primitiveRot = transform.eulerAnglesl;
        tempPos = Vector3.zero;
        // targetPos = new Vector3(2,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            MoveTo(targetPos,distantce);
        }
    }

    protected override void MoveTo(Vector3 go,float dis)
    {
        
       if(Vector3.Distance(transform.position,go)>dis){
           tempPos = go; //让目标点等高
           //tempPos.y = transform.position.y;
          
           transform.LookAt(tempPos);
           transform.Translate(Vector3.forward * Time.deltaTime *5);

       }else{//否则就是到达，是否攻击，还是返回
            canMove = false;
            if(dis>0.1f){
                Debug.Log("attack");
                //攻击
                canMove = true;
                targetPos = primitivePos;
                distantce = 0.1f;
            }else{
                //返回原始位置
                Debug.Log("goback");
                GoBack();
            }

       }
    }

    private void GoBack(){
        transform.position = primitivePos;
        // transform.eulerAngles = primitiveRot;
        canMove = false;
    }
}
