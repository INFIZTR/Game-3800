using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public int maxSize;

    // itemlist should be shared across scene
    public List<CollectableItem> itemList = new List<CollectableItem>();
    
    public bool used = false;
    //public InventoryManager inventoryManager;

    private void Start()
    {
        Debug.Log(itemList.Count);
        InventoryManager.RefreshInventory();
    }


    public void AddNew(CollectableItem thisItem)
    {
        if (thisItem == null)
        {
            //Debug.Log(11122);
            return;
        }

        if (!itemList.Contains(thisItem))
        {
            CollectableItem newItem = thisItem.Copy();
            itemList.Add(newItem);
            //Debug.Log(itemList.Count);

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
}
