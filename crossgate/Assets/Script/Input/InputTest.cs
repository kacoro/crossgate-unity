using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //开启输入检测
        InputMgr.GetInstance().StartOrEndCheck(true);
        //添加事件监听
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyisDown", CheckInputDown);
        EventCenter.GetInstance().AddEventListener<KeyCode> ("KeyisUp", CheckInputUp);
    }
    private void CheckInputDown(KeyCode obj) {
        switch (obj) {
            case KeyCode.W:
                Debug.Log("前进");
                break;
            case KeyCode.A:
                break;
            case KeyCode.S:
                break;
            case KeyCode.D:
                break;
        }
    }
    private void CheckInputUp(KeyCode obj) {
        switch (obj)
        {
            case KeyCode.W:
                Debug.Log("不再前进");
                break;
            case KeyCode.A:
                break;
            case KeyCode.S:
                break;
            case KeyCode.D:
                break;
        }
    }
    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("KeyisDown", CheckInputDown);
        EventCenter.GetInstance().RemoveEventListener<KeyCode>("KeyisUp", CheckInputUp);
    }

}
