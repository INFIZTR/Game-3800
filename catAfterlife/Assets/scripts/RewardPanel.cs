using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardPanel : MonoBehaviour
{
    public GameObject blockDestroyedText;

    public void Hide()
    {
        gameObject.SetActive(false);
        if (blockDestroyedText != null)
        {
            blockDestroyedText.SetActive(true);
        }
    }
}
