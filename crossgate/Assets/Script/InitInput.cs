using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InitInput : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject lastSelect;

    void Start()
    {
        lastSelect = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null){
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }else{
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
