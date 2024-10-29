using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpriteSwapper : MonoBehaviour
{
    public Image image1;         // First image
    public Image image2;         // Second image
    public float fadeDuration = 2.0f; // Duration of the fade

    private bool isFading = false;

    void Start()
    {
        if (image1 == null || image2 == null)
        {
            Debug.LogError("Please assign both images in the Inspector.");
            return;
        }

        // Set initial alpha values
        SetAlpha(image1, 1f);
        SetAlpha(image2, 0f);

        // Start the continuous fade loop
        StartCoroutine(FadeImagesLoop());
    }

    private IEnumerator FadeImagesLoop()
    {
        while (true)
        {
            // Fade out image1 and fade in image2
            yield return StartCoroutine(FadeImages(image1, image2));

            // Swap images and repeat the fade
            yield return StartCoroutine(FadeImages(image2, image1));
        }
    }

    private IEnumerator FadeImages(Image fadeOutImage, Image fadeInImage)
    {
        isFading = true;
        float timer = 0f;

        // Start fade-out of fadeOutImage and fade-in of fadeInImage
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = timer / fadeDuration;

            SetAlpha(fadeOutImage, 1f - alpha);
            SetAlpha(fadeInImage, alpha);

            yield return null;
        }

        // Ensure the final state is set
        SetAlpha(fadeOutImage, 0f);
        SetAlpha(fadeInImage, 1f);

        isFading = false;
    }

    private void SetAlpha(Image img, float alpha)
    {
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }
}
