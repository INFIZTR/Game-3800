using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PosionManager : MonoBehaviour
{
    static PosionManager instance;
    
    public Inventory inventory;
    public GameObject slotGtid;
    public SlotManager slot;

    private AudioSource aud;

    // play the background music
    private GameObject bgPlayer;

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
        RefreshPosionList();
        aud = GetComponent<AudioSource>();
        aud.Play();
        bgPlayer = GameObject.FindGameObjectWithTag("BackGroundMusic");
        bgPlayer.SetActive(false);
    }

    private void OnDisable()
    {
        aud.Stop();
        // resume playing bg music
        if (bgPlayer != null)
        {
            bgPlayer.SetActive(true);
        }
    }

    public static void CreatePosionList(CollectableItem item)
    {
        SlotManager newSlot = Instantiate(instance.slot, instance.slotGtid.transform.position,Quaternion.identity);
        newSlot.gameObject.transform.SetParent(instance.slotGtid.transform);
        newSlot.slotItem = item;
        newSlot.slotImage.sprite = item.itemSprite;
        newSlot.slotNumber.text = item.itemNumber.ToString();
    }

    public static void RefreshPosionList()
    {
        for (int i = instance.slotGtid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(instance.slotGtid.transform.GetChild(i).gameObject);
        }

        for (int j = 0; j < instance.inventory.itemList.Count; j++)
        {
            CreatePosionList(instance.inventory.itemList[j]);
        }
    }

    public static bool UseOneThing(CollectableItem thisItem)
    {
        return instance.inventory.UseOnce(thisItem);
    }
    
    public static void ReturnOneThing(CollectableItem thisItem)
    {
        instance.inventory.ReturnUse(thisItem);
    }

    public void Start()
    {
       
    }

    public void Update()
    {
        //RefreshPosionList();
    }
}
