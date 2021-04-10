using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemName;
    public Sprite itemSprite;

    public string itemDesc;

    public Item(string item, Sprite sprite, string desc)
    {
        itemName = item;
        itemSprite = sprite;
        itemDesc = desc;
    }
}
