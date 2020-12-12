using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
  private Inventory inventory;
  private Transform itemSlotContainer;
  private Transform itemSlotTemplate;

  private void Awake() { //没有顺序执行~
      
  }
  public void init(){
  
  }

  public void SetInventory(Inventory inventory){
      itemSlotContainer = transform.Find("itemSlotContainer");
      itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
      this.inventory = inventory;
      inventory.OnItemListChanged += Inventory_OnItemListChanged;
      RefreshInventoryItems();
  }

  private void Inventory_OnItemListChanged(object sender,System.EventArgs e){
    RefreshInventoryItems();
  }

  public void RefreshInventoryItems(){
    foreach(Transform child in itemSlotContainer){
      if(child == itemSlotTemplate) continue;
      Destroy(child.gameObject);
    }
    //   int x = 0;
    //   int y = 0;
    //   float itemSlotCellSize = 75f;
      foreach (Item item in inventory.GetItemList()){
          Debug.Log(3);
          RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate,itemSlotContainer).GetComponent<RectTransform>();
          Debug.Log(itemSlotRectTransform);
          itemSlotRectTransform.gameObject.SetActive(true);
          Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
          Text text =  itemSlotRectTransform.Find("Text").GetComponent<Text>();
          if(item.amount>1){
            text.text = item.amount.ToString();
          }else{
            text.text = "";
          }
          
          image.sprite = item.GetSprite();
          image.SetNativeSize();
        //   itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize,y* itemSlotCellSize );
        //   x++;
        //   if(x>5){
        //       x=0;
        //       y++;
        //   }
      }
  }

}
