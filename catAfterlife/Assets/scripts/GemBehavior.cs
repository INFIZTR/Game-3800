using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
{
    public float fadeDuration = 1f; 

    public void LeaveScene()
    {
        StartCoroutine(FadeOutAndDeactivate());
    }

    private IEnumerator FadeOutAndDeactivate()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            float elapsedTime = 0f;

            // Gradually reduce the alpha value
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            // Ensure the sprite is fully transparent
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        }

        // Deactivate the GameObject after the fade-out
        gameObject.SetActive(false);
    }
}
