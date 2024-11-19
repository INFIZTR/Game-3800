using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleTriggerBehavior : MonoBehaviour
{
    public GameObject levelManager;
    [Tooltip("when collides, tp to which direction\ntop0, left1, bottom2, right3")]
    public int directionIndicator = 0;

    public GameObject Fkey;

    private void Start()
    {
        Fkey.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Fkey.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                var lm = levelManager.GetComponent<LevelManager>();
                lm.LevelSwitch(directionIndicator);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Fkey.SetActive(false);
    }


}
