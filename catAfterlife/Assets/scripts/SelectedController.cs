using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedController : MonoBehaviour
{
    static SelectedController instance;
    public List<CollectableItem> SelectList = new List<CollectableItem>();
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
}
