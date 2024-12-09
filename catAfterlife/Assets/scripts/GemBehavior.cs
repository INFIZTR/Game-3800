using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
{
    public float fadeDuration = 1f;      
    public float moveDistance = 5f;    
    public float moveSpeed = 2f;  
    public bool trigger = false;

    private TalkController talkController;

    // if the player already handin the potion
    public static bool afterPotion = false;

    private void Update()
    {
        // can delete, simply for easier to test
        if (trigger)
        {
            LeaveScene();
            trigger = false;
        }
    }

    public void LeaveScene()
    {
        Debug.Log("leave!");
        StartCoroutine(FadeOutAndDeactivate());
    }

    private IEnumerator FadeOutAndDeactivate()
    {
        afterPotion = true;

        if (talkController == null)
        {
            talkController = GetComponent<TalkController>();
        }
        talkController.Leaving();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;

            // original and target positions
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = originalPosition + new Vector3(moveDistance, 0f, 0f);

            // calculate the total time needed to reach the target based on speed
            float moveDuration = moveDistance / moveSpeed;

            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                elapsedTime += Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                yield return null;
            }

            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            transform.position = targetPosition;
        }


        gameObject.SetActive(false);
    }

    // return the status of gem, true if gem already received potion
    public bool GetStatusOfGem()
    {
        return afterPotion;
    }
}
