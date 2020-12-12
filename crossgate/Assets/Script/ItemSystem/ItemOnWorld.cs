using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public ItemBase item;
    public InventoryBase playerInventory;

    private void OnTriggerEnter2D(Collider2D other) {
         Debug.Log("Enter trigger");
         if(other.gameObject.CompareTag("Player")){
            AddNewItem();
            Destroy(gameObject);
         }   
    }

    public void AddNewItem(){
        if(!playerInventory.ItemList.Contains(item)){
            playerInventory.ItemList.Add(item);
        }else{
            item.amount +=1;
        }
    }
}
