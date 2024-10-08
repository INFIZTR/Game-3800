using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{
    public GameObject Button;
    public GameObject TalkingGUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
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
            TalkingGUI.SetActive(true);
        }
    }
}