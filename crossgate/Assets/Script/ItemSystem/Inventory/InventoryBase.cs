using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory/Create New Inventory")]
public class InventoryBase :ScriptableObject
{
    // Start is called before the first frame update
    public List<ItemBase> ItemList = new List<ItemBase>();
}
