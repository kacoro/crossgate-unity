using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //   textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }
    private void Start() {
        
    }
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Debug.Log(ItemAssets.Instance.pfItemWorld);
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        Debug.Log(item.amount);
      
        if(item.amount >1){
             textMeshPro.SetText(item.amount.ToString());
        }else{
             textMeshPro.SetText("");
        }
    }
    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
