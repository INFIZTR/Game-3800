using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehavior : MonoBehaviour
{
    public GameObject levelManager;
    [Tooltip("when collides, tp to which direction\ntop0, left1, bottom2, right3")]
    public int directionIndicator = 0;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var lm  = levelManager.GetComponent<LevelManager>();
            lm.LevelSwitch(directionIndicator);
        }
    }
}
