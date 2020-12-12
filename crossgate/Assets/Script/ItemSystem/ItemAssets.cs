using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    public Transform pfItemWorld;
    public Sprite swordSprite;
    public Sprite healThpotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite meditSprite;
    private void Awake()
    {
        Instance = this;
    }
}
