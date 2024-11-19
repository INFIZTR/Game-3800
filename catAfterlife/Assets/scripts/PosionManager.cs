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
    public static int numOfIngradient = 0;
    private AudioSource aud;
    
    public static TMP_Text text;

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
        GameObject obj = GameObject.Find("Descriptions");
        if (obj != null)
        {
            text = obj.GetComponent<TMP_Text>();
        }
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
        newSlot.description = item.description;
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
        if (numOfIngradient < 4)
        {
            numOfIngradient++;
            return instance.inventory.UseOnce(thisItem);
        }
        else
        {
            return false;
        }
    }
    
    public static void ReturnOneThing(CollectableItem thisItem)
    {
        numOfIngradient--;
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
