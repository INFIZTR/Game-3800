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
    public GameObject slotGtid;
    public SlotManager slot;
    public bool isopen = false;
    public RectTransform inventoryUI;
    public Vector3 upperPosition;
    public Vector3 lowerPosition;
    public float animationTime;
    

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
        if (item.itemNumber != 0)
        {
            SlotManager newSlot = Instantiate(instance.slot, instance.slotGtid.transform.position,Quaternion.identity);
            newSlot.gameObject.transform.SetParent(instance.slotGtid.transform);
            newSlot.slotItem = item;
            newSlot.slotImage.sprite = item.itemSprite;
            newSlot.slotNumber.text = item.itemNumber.ToString();
        }
    }

    public static void RefreshInventory()
    {
        for (int i = instance.slotGtid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(instance.slotGtid.transform.GetChild(i).gameObject);
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
