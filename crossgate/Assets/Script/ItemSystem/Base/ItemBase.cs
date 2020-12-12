using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Create New Item")]
public class ItemBase : ScriptableObject
{
    public string name;
    public Sprite images;
    public int amount = 1;
    [TextArea]
    public string description;

    public bool equipable;
}
