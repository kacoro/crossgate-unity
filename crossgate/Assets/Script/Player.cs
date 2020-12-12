using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public int level = 3;
   public int health = 4;
   public string sceneName = "SampleScene";

    public static Player instance;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    private void Awake() {
          if(instance !=null)
            Destroy(this);
        instance = this;
        inventory = new Inventory(UseItem);
        if(uiInventory){
            uiInventory.SetInventory(inventory);
        }
        
       
        // ItemWorld.SpawnItemWorld(new Vector3(-4,4),new Item{itemType = Item.ItemType.HealthPotion, amount =1 });
        // ItemWorld.SpawnItemWorld(new Vector3(-6,2),new Item{itemType = Item.ItemType.ManaPotion, amount =1 });
        // ItemWorld.SpawnItemWorld(new Vector3(-5,4),new Item{itemType = Item.ItemType.Sword, amount =1 });
    }

    private void UseItem(Item item){
        switch(item.itemType){
            case Item.ItemType.HealthPotion:
            inventory.RemoveItem(new Item {itemType = Item.ItemType.HealthPotion,amount = 1});
            break;
            case Item.ItemType.ManaPotion:
            inventory.RemoveItem(new Item {itemType = Item.ItemType.ManaPotion,amount = 1});
            break;
        }

    }

     private void OnTriggerEnter2D(Collider2D collider) {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
         Debug.Log(itemWorld);
        if(itemWorld!=null){
            Debug.Log(itemWorld);
            //touching Item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

   public void SavePlayer(){
       Debug.Log("save");
       SaveSystem.SavePlayer(this);
   }

    // static void Load(){
    //    PlayerData data = SaveSystem.LoadPlayer();
    //    instance.level = data.level;
    //    instance.health = data.health;
    //    instance.sceneName = data.sceneName;  
    //    Vector3 position;
    //     FindObjectOfType<SceneFader>().FadeTo(instance.sceneName);
    //    position.x = data.postion[0];
    //    position.y = data.postion[1];
    //    position.z = data.postion[2];
    //    instance.transform.position = position;
    // }

   public void LoadPlayer(){
        Debug.Log("load");
       PlayerData data = SaveSystem.LoadPlayer();

       level = data.level;
       health = data.health;
       sceneName = data.sceneName;  
       Vector3 position;
        FindObjectOfType<SceneFader>().FadeTo(sceneName);
       position.x = data.postion[0];
       position.y = data.postion[1];
       position.z = data.postion[2];
       transform.position = position;
   }
}
