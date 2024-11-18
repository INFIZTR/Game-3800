using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject CanvesGUI;
    bool firstTime;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime)
        {
            firstTime = false;
            CanvesGUI.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CanvesGUI.SetActive(false);
            }
        }
    }
}
