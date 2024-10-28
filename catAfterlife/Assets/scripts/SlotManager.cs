using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotManager : MonoBehaviour
{
   public CollectableItem slotItem;
   public Image slotImage;
   public TMP_Text slotNumber;
   
   //public PosionManager posionManager;
   
   public void ItemOnClickedL()
   {
      bool used = PosionManager.UseOneThing(slotItem);
      if (used)
      {
         SelectedController.AddList(slotItem);
      }
   }
   public void ItemOnClickedR()
   {
      Debug.Log("ClickedR");
      bool success = SelectedController.RemoveList(slotItem);
      Debug.Log(success);
      if (success)
      {
         PosionManager.ReturnOneThing(slotItem);
      }
   }
   
}
