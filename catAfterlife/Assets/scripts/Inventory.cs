using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    static Inventory instance;
    public int maxSize;
    public List<CollectableItem> itemList = new List<CollectableItem>();
    
    public bool used = false;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    
    public static void AddNew(CollectableItem thisItem)
    {
        if (!instance.itemList.Contains(thisItem))
        {
            CollectableItem newItem = thisItem.Copy();
            instance.itemList.Add(newItem);
             
        }
        else
        {
            instance.itemList.Find(item => item.itemName == thisItem.itemName).itemNumber = 
                instance.itemList.Find(item => item.itemName == thisItem.itemName).itemNumber + thisItem.itemNumber;
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
