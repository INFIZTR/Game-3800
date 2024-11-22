using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public int dialogIndex = 0;
    private string[] dialogRows;

    // list of texts for npc
    public TextAsset textAsset_default;
    // load if the player is talking to the npc for the second time
    public TextAsset textAsset_second;
    // if the player got the item requested
    public TextAsset textAsset_withItem;

    // current text to be displayed
    private TextAsset currentText;
    public Image spriteL;
    public Image spriteR;


    public TMP_Text nameText;
    public TMP_Text dialogText;
    
    public List<Image> sprites = new List<Image>();
    Dictionary<string, Image> nameImageDict = new Dictionary<string, Image>();

    [FormerlySerializedAs("istext")] public bool isText;
    
    public GameObject dialogButton;
    public Transform dialogButtonGroup;

    public GameObject TalkingGUI;
    public GameObject InventoryGUI;

    public GameObject player;

    // npc required item
    public CollectableItem requiredItem;

    // count how many times have the dialogue been invoked
    private int triggerCount = 0;


    // Start is called before the first frame update
    void Start()
    {

        currentText = textAsset_default;
        GenerateText(currentText);

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // helper method to find if a certain item is inside player's inventory
    private bool containsItem()
    {
        var invs = InventoryGUI.GetComponent<InventoryManager>();
       
        return invs.inventory.itemList.Contains(requiredItem);
    }
    public void Awake()
    {
        triggerCount = 0;
        nameImageDict["Witch"] = sprites[0];
        //This is for the darker version of the opposite image
        nameImageDict["WitchOP"] = sprites[3];
        nameImageDict["Turtle"] = sprites[2];
        //This is for the darker version of the opposite image
        nameImageDict["TurtleOP"] = sprites[1];
    }

    private void OnEnable()
    {
        // check if player already acquired speacial item,
        // if so display new button
        if (containsItem())
        {
            currentText = textAsset_withItem;
        }
        // if this is the second time load text
        else if (triggerCount > 0)
        {
            currentText = textAsset_second;
        }
        else
        {
            currentText = textAsset_default;
        }

        //Debug.Log(triggerCount);

        // lock player's movement
        var playerS = player.GetComponent<PlayerMovement>();
        playerS.LockMovement();

      
    }

    private void OnDisable()
    {
        // unlock player's movement
        if (player != null)
        {
            var playerS = player.GetComponent<PlayerMovement>();
            playerS.UnlockMovement();
        }
    }

    private void UpdateText(string _name, string _dialog)
    {
        nameText.text = _name;
        dialogText.text = _dialog;
    }

    private void UpdateImage(string _name, string position)
    {
        string darkerOpposite = _name + "OP";
        if (position == "L")
        {
            spriteL.gameObject.SetActive(true);
            spriteR.gameObject.SetActive(false);
            //spriteL = nameImageDict[_name];
            //spriteR = nameImageDict[darkerOpposite];
        }
        else if (position == "R")
        {
            spriteL.gameObject.SetActive(false);
            spriteR.gameObject.SetActive(true);
            //spriteR = nameImageDict[_name];
            //spriteL = nameImageDict[darkerOpposite];
        }
    }
    
    private void GenerateText(TextAsset dialog)
    {
        dialogRows = dialog.text.Split('\n');
        PrintText();
    }

    private void PrintText()
    {
        for (int i = 0; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split('\t');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                isText = true;
                UpdateText(cells[2], cells[4]);
                UpdateImage(cells[2], cells[3]);

                dialogIndex = int.Parse(cells[5]);
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                isText = false;
                GenerateBotton(i);
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                // increment the counter
                triggerCount++;
                dialogIndex = 0;
                Start();
                TalkingGUI.SetActive(false);
                InventoryGUI.SetActive(true);
            }
        }
    }

    private void GenerateBotton(int index)
    {
        string[] cells = dialogRows[index].Split('\t');

        //string[] cells = dialogRows[index].Split(',');
        //string[] cells = dialogRows[index].Split(new[] { ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (cells[0] == "&")
        {
            GameObject button = Instantiate(dialogButton,dialogButtonGroup);
            button.GetComponentInChildren<TextMeshProUGUI>().text = cells[4];
            button.GetComponent<Button>().onClick.AddListener(delegate { OnButtonClick(int.Parse(cells[5]));});
            GenerateBotton(index+1);
        }
    }

    private void OnButtonClick(int id)
    {
        dialogIndex = id;
        PrintText();
        for (int i = 0; i < dialogButtonGroup.childCount; i++)
        {
            Destroy(dialogButtonGroup.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) && isText)
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && isText)
        {
            GenerateText(currentText);
        }
    }
}
