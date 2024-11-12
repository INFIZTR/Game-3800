using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDestroyedTextBehavior : MonoBehaviour
{
    public float time = 2f;
    private void OnEnable()
    {
        // Start the coroutine to deactivate after 2 seconds
        StartCoroutine(DeactivateAfterTime(time));
    }

    // Coroutine to deactivate the GameObject
    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
