using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDestroyedTextBehavior : MonoBehaviour
{
    public float time = 2f;
    bool invoked = false;

    private void OnEnable()
    {
        if (!invoked)
        {
            // Start the coroutine to deactivate after 2 seconds
            StartCoroutine(DeactivateAfterTime(time));
            invoked = true;
        }

    }

    // Coroutine to deactivate the GameObject
    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
