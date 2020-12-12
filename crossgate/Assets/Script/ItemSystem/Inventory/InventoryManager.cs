using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public InventoryBase myBag;

    
    

    private void Awake() {
        if(instance !=null)
            Destroy(this);
        instance = this;
   }
}
