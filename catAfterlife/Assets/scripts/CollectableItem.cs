using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public int itemNumber;

    public bool isPosion;

    public override bool Equals(object item)
    {
        if (item == null || !(item is CollectableItem))
        {
            return false;
        }
        
        CollectableItem other = (CollectableItem)item;
        return this.itemName == other.itemName;
    }

    public CollectableItem Copy()
    {
        CollectableItem newItem = new CollectableItem();
        newItem.itemName = this.itemName;
        newItem.itemNumber = this.itemNumber;
        newItem.isPosion = this.isPosion;
        newItem.itemSprite = this.itemSprite;
        return newItem;
    }

    public void destoryItself()
    {
        Destroy(gameObject);
    }
    
}
