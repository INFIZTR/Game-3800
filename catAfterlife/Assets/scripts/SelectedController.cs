using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedController : MonoBehaviour
{
    public Inventory inventory;
    static SelectedController instance;
    public List<CollectableItem> SelectList = new List<CollectableItem>();
    public GameObject slotGtid;
    public SlotManager slot;
    
    public CollectableItem posion;
    public GameObject posionGUI;
    public GameObject inventorySystemGUI;
    
    public TextAsset posionRecipe;
    private string[] recipeRows;
    public List<CollectableItem> posionList = new List<CollectableItem>();
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    
    public static void RefreshList()
    {
        for (int i = instance.slotGtid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(instance.slotGtid.transform.GetChild(i).gameObject);
        }

        for (int j = 0; j < instance.SelectList.Count; j++)
        {
            CreatePosionList(instance.SelectList[j]);
        }
    }
    

    public static void AddList(CollectableItem thisItem)
    {
        if (instance.SelectList.Count < 4)
        {
            CreatePosionList(thisItem);
            instance.SelectList.Add(thisItem);
        }
    }
    public static void CreatePosionList(CollectableItem item)
    {
        SlotManager newSlot = Instantiate(instance.slot, instance.slotGtid.transform.position,Quaternion.identity);
        newSlot.gameObject.transform.SetParent(instance.slotGtid.transform);
        newSlot.slotItem = item;
        newSlot.slotImage.sprite = item.itemSprite;
    }
    
    public static bool RemoveList(CollectableItem thisItem)
    {
        if (instance.SelectList.Count > 0)
        {
            Debug.Log(instance.SelectList.Count);
            bool success = instance.SelectList.Remove(thisItem);
            Debug.Log(success);
            Debug.Log(instance.SelectList.Count);
            RefreshList();
            return true;
        }

        RefreshList();
        return false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            inventorySystemGUI.SetActive(true);
            posionGUI.SetActive(false);
        }
    }
    
    public void ReadRecipe()
    {
        recipeRows = posionRecipe.text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 1; i < recipeRows.Length; i++)
        {
            bool isPosion = true;
            string[] cells = recipeRows[i].Split('\t');
            for (int j = 0; j < 3; j++) {
                if (!(instance.SelectList[j].itemName == cells[j]))
                {
                    isPosion = false;
                }
            }
            inventorySystemGUI.SetActive(true);
            if (isPosion)
            {
                SelectList.Clear();
                RefreshList();
                inventory.AddNew(posionList.Find(x => x.itemName == cells[4]));
                posionGUI.SetActive(false);
                break;
            }
        }
    }

    /*public void MakePosion()
    {
        bool isPosion = true;
        for (int i = 0; i < 4; i++) {
            if (!(instance.SelectList[i].itemName == "test item"))
            {
                isPosion = false;
            }
        }
        inventorySystemGUI.SetActive(true);
        if (isPosion)
        {
            SelectList.Clear();
            RefreshList();
            inventory.AddNew(posion);
        }
        
        posionGUI.SetActive(false);
        //inventorySystemGUI.SetActive(true);
    }*/
}
