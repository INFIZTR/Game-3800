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
    public TextAsset textAsset;
    public SpriteRenderer spriteL;
    public SpriteRenderer spriteR;


    public TMP_Text nameText;
    public TMP_Text dialogText;
    
    public List<Sprite> sprites = new List<Sprite>();
    Dictionary<string, Sprite> nameImageDict = new Dictionary<string, Sprite>();

    [FormerlySerializedAs("istext")] public bool isText;
    
    public GameObject dialogButton;
    public Transform dialogButtonGroup;

    public GameObject TalkingGUI;
    public GameObject InventoryGUI;

    public void Awake()
    {
        nameImageDict["Witch"] = sprites[0];
        //This is for the darker version of the opposite image
        nameImageDict["WitchOP"] = sprites[3];
        nameImageDict["Cat"] = sprites[2];
        //This is for the darker version of the opposite image
        nameImageDict["CatOP"] = sprites[1];
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
            spriteL.sprite = nameImageDict[_name];
            spriteR.sprite = nameImageDict[darkerOpposite];
        }
        else if (position == "R")
        {
            spriteR.sprite = nameImageDict[_name];
            spriteL.sprite = nameImageDict[darkerOpposite];
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
                dialogIndex = 0;
                Start();
                TalkingGUI.SetActive(false);
                InventoryGUI.SetActive(true);
            }
        }
    }

    private void GenerateBotton(int index)
    {
        string[] cells = dialogRows[index].Split(',');
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(dialogButton,dialogButtonGroup);
            button.GetComponentInChildren<TMP_Text>().text = cells[4];
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

    // Start is called before the first frame update
    void Start()
    {
        GenerateText(textAsset);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) && isText)
        {
            GenerateText(textAsset);
        }
    }
}
