using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public CollectableItem slotItem;
   public Image slotImage;
   public TMP_Text slotNumber;
   
   public string description;
   
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
      //Debug.Log("ClickedR");
      bool success = SelectedController.RemoveList(slotItem);
      //Debug.Log(success);
      if (success)
      {
         PosionManager.ReturnOneThing(slotItem);
      }
   }


   public void OnPointerEnter(PointerEventData eventData)
   {
      PosionManager.text.text = description;
   }

   public void OnPointerExit(PointerEventData eventData)
   {
      PosionManager.text.text = "";
   }
}
