using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;

    private void Start() {
        // ItemWorld.SpawnItemWorld(new Vector3(-4,4),new Item{itemType = Item.ItemType.HealthPotion, amount =1 });
        ItemWorld.SpawnItemWorld(transform.position,item);
        Destroy(gameObject);
    }

}
