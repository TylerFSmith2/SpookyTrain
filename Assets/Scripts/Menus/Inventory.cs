using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] items = new Item[8];

    public GameObject[] buttons;

    public GameObject itemDescriptionPanel;
    public Text itemNameText;
    public Text itemDescriptionText;

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemToAdd;
                buttons[i].GetComponent<Image>().sprite = itemToAdd.itemSprite;
                return true;
            }
        }
        Debug.Log("Inventory Full");
        return false;
    }

    public void ClickItem(int slot)
    {
        //TODO: Fix inventory bug -- Can't see items
        Debug.Log(items[slot].itemName);
    }

    public void MouseOverItem(int slot)
    {
        if(items[slot] == null)
        {
            return;
        }
        itemDescriptionPanel.SetActive(true);
        itemNameText.text = items[slot].itemName;
        itemDescriptionText.text = items[slot].itemDesc;
    }

    public void MouseExit()
    {
        itemDescriptionPanel.SetActive(false);
    }

    public bool CheckForAndUseItem(string itemToFind)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                continue;
            }
            if (items[i].itemName == itemToFind)
            {
                items[i] = null;
                return true;
            }
        }
        return false;
    }
}
