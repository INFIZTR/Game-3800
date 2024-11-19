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

    public void Awake()
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
        if (item.itemNumber != 0)
        {
            SlotManager newSlot = Instantiate(instance.slot, instance.slotGrid.transform.position,Quaternion.identity);
            newSlot.gameObject.transform.SetParent(instance.slotGrid.transform);
            newSlot.gameObject.transform.localScale = Vector3.one;
            newSlot.slotItem = item;
            newSlot.slotImage.sprite = item.itemSprite;
            //Instantiate(newSlot.slotImage.sprite);
            newSlot.slotNumber.text = item.itemNumber.ToString();
            newSlot.description = item.description;
            //Debug.Log(item.description);

        }
    }

    public static void RefreshInventory()
    {
        for (int i = instance.slotGrid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int j = 0; j < instance.inventory.itemList.Count; j++)
        {
            CreateInventory(instance.inventory.itemList[j]);
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
