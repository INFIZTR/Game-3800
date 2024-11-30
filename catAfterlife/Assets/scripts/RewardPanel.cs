using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    public GameObject blockDestroyedText;
    public bool alreadyInvoked = false;

    public void Hide()
    {
        gameObject.SetActive(false);
        if (blockDestroyedText != null)
        {
            blockDestroyedText.SetActive(true);
        }

        // the player can only collect rare item for 1 time
        alreadyInvoked = true;
    }
}
