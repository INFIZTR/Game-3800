using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
{
    public float fadeDuration = 1f; 
    public float moveDistance = 5f; 

    public void LeaveScene()
    {
        Debug.Log("leave!");
        StartCoroutine(FadeOutAndDeactivate());
    }

    private IEnumerator FadeOutAndDeactivate()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            float elapsedTime = 0f;

            // Original and target positions
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = originalPosition + new Vector3(moveDistance, 0f, 0f);

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / fadeDuration;

                // Easing function (optional)
                float easeT = t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;

                // Fade out
                float alpha = Mathf.Lerp(1f, 0f, t);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                // Move to the right with easing
                transform.position = Vector3.Lerp(originalPosition, targetPosition, easeT);

                yield return null;
            }

            // Ensure final state
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            transform.position = targetPosition;
        }

        // Deactivate the GameObject after the fade-out
        gameObject.SetActive(false);
    }
}
