using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSize;
    public List<CollectableItem> itemList = new List<CollectableItem>();
    
    public bool used = false;
    
    
    private void AddNew(CollectableItem thisItem)
    {
        if (!itemList.Contains(thisItem))
        {
            CollectableItem newItem = thisItem.Copy();
            itemList.Add(newItem);
             
        }
        else
        {
            itemList.Find(item => item.itemName == thisItem.itemName).itemNumber = 
                itemList.Find(item => item.itemName == thisItem.itemName).itemNumber + thisItem.itemNumber;
        }

        InventoryManager.RefreshInventory();
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
            AddNew(thisItem);
            thisItem.destoryItself();
        }
    }
    
    
}
