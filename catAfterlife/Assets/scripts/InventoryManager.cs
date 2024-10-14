using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    
    public Inventory inventory;
    public GameObject slotGtid;
    public SlotManager slot;
    

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void OnEnable()
    {
        RefreshInventory();
    }

    public static void CreateInventory(CollectableItem item)
    {
        SlotManager newSlot = Instantiate(instance.slot, instance.slotGtid.transform.position,Quaternion.identity);
        newSlot.gameObject.transform.SetParent(instance.slotGtid.transform);
        newSlot.slotItem = item;
        newSlot.slotImage.sprite = item.itemSprite;
        newSlot.slotNumber.text = item.itemNumber.ToString();
    }

    public static void RefreshInventory()
    {
        for (int i = 0; i < instance.slotGtid.transform.childCount; i++)
        {
            if (instance.slotGtid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGtid.transform.GetChild(i).gameObject);
        }

        for (int j = 0; j < instance.inventory.itemList.Count; j++)
        {
            CreateInventory(instance.inventory.itemList[j]);
        }
    }
}
