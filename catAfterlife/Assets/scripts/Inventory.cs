using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSize;

    // itemlist should be shared across scene
    static public List<CollectableItem> itemList = new List<CollectableItem>();
    
    public bool used = false;
    public InventoryManager inventoryManager;

    private void Start()
    {
        Debug.Log(itemList.Count);
        inventoryManager.RefreshInventory();
    }


    public void AddNew(CollectableItem thisItem)
    {
        if (thisItem == null)
        {
            return;
        }

        if (!itemList.Contains(thisItem))
        {
            CollectableItem newItem = thisItem.Copy();
            itemList.Add(newItem);
            Debug.Log(itemList.Count);

        }
        else
        {
            itemList.Find(item => item.itemName == thisItem.itemName).itemNumber = 
            itemList.Find(item => item.itemName == thisItem.itemName).itemNumber + thisItem.itemNumber;
        }

        inventoryManager.RefreshInventory();
    }

    public bool UseOnce(CollectableItem thisItem)
    {
        used = false;
        if (itemList.Find(item => item.itemName == thisItem.itemName).itemNumber >= 1)
        {
            itemList.Find(item => item.itemName == thisItem.itemName).itemNumber--;
            PosionManager.RefreshPosionList();
            used = true;
        }
        return used;
    }
    
    public void ReturnUse(CollectableItem thisItem)
    {
        itemList.Find(item => item.itemName == thisItem.itemName).itemNumber++;
        PosionManager.RefreshPosionList();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") && itemList.Count < maxSize)
        {
            CollectableItem thisItem = collision.gameObject.GetComponent<CollectableItem>();

            // if player has collide with the CollectableItem, collect it
            AddNew(thisItem);
            thisItem.destoryItself();
        }
    }
    
    
}
