using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public int itemNumber;

    public bool isPosion;
    
    public Inventory inventory;

    public override bool Equals(object item)
    {
        if (item == null || !(item is CollectableItem))
        {
            return false;
        }
        
        CollectableItem other = (CollectableItem)item;
        return this.itemName == other.itemName;
    }

    public CollectableItem Copy()
    {
        CollectableItem newItem = new CollectableItem();
        newItem.itemName = this.itemName;
        newItem.itemNumber = this.itemNumber;
        newItem.isPosion = this.isPosion;
        newItem.itemSprite = this.itemSprite;
        return newItem;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && inventory.itemList.Count < inventory.maxSize)
        {
            CollectableItem thisItem = collision.gameObject.GetComponent<CollectableItem>();

            // if player has collide with the CollectableItem, collect it
            inventory.AddNew(thisItem);
            thisItem.destoryItself();
        }
    }

    public void destoryItself()
    {
        Destroy(gameObject);
    }
    
}
