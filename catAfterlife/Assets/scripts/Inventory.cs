using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSize;
    public List<CollectableItem> itemList = new List<CollectableItem>();
    
    private void AddNew(CollectableItem thisItem)
    {
        if (!itemList.Contains(thisItem))
        {
            CollectableItem newItem = thisItem.Copy();
            itemList.Add(newItem);
            //Debug.LogWarning("Adding " + thisItem.name + " to Inventory");
            //Debug.LogWarning(thisItem.name + " has " + itemList.Find(item => item.itemName == thisItem.itemName).itemNumber + " items in Inventory");
        }
        else
        {
            itemList.Find(item => item.itemName == thisItem.itemName).itemNumber = 
                itemList.Find(item => item.itemName == thisItem.itemName).itemNumber + thisItem.itemNumber;
            //Debug.LogWarning(thisItem.name + " has " + itemList.Find(item => item.itemName == thisItem.itemName).itemNumber + " items in Inventory");
            
        }

        InventoryManager.RefreshInventory();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") && itemList.Count < maxSize)
        {
            CollectableItem thisItem = collision.gameObject.GetComponent<CollectableItem>();
            AddNew(thisItem);
            thisItem.destoryItself();
        }
    }
    
    
}
