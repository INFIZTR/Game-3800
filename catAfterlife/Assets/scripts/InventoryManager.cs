using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    
    public Inventory inventory;
    public GameObject slotGrid;
    public SlotManager slot;
    public bool isopen = false;
    public RectTransform inventoryUI;

    // position of inventory when player has pressed "E"
    public Vector3 upperPosition;
    // default position of inventory
    public Vector3 lowerPosition;
    public float animationTime;
    
    private void OnEnable()
    {
        RefreshInventory();
    }

    public void CreateInventory(CollectableItem item)
    {
        if (item.itemNumber != 0)
        {
            SlotManager newSlot = Instantiate(slot, slotGrid.transform.position,Quaternion.identity);
            newSlot.gameObject.transform.SetParent(slotGrid.transform);
            newSlot.gameObject.transform.localScale = Vector3.one;
            newSlot.slotItem = item;
            newSlot.slotImage.sprite = item.itemSprite;
            //Instantiate(newSlot.slotImage.sprite);
            newSlot.slotNumber.text = item.itemNumber.ToString();
            Debug.Log(item.itemSprite.ToString());

        }
    }

    public void RefreshInventory()
    {
        for (int i = slotGrid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(slotGrid.transform.GetChild(i).gameObject);
        }

        for (int j = 0; j < Inventory.itemList.Count; j++)
        {
            CreateInventory(Inventory.itemList[j]);
        }
    }

    public void Start()
    {
        lowerPosition = inventoryUI.anchoredPosition;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isopen)
            {
                inventoryUI.DOAnchorPos(upperPosition, animationTime);
                isopen = true;
            }
            else
            {
                inventoryUI.DOAnchorPos(lowerPosition, animationTime);
                isopen = false;
            }
        }
    }
}
