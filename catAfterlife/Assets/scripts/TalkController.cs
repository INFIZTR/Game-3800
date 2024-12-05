using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{
    public GameObject Button;
    public GameObject CanvesGUI;
    public GameObject inventorySystemGUI;

    //for gem rock, set talk function inactive while rocks moving
    private bool leavingScene = false;

    public void Leaving()
    {
        leavingScene = true;
    }

    private void Start()
    {
        CanvesGUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!leavingScene)
        {
            Button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            CanvesGUI.SetActive(true);
            inventorySystemGUI.SetActive(false);
        }
    }
}
