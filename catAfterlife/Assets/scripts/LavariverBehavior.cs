using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavariverBehavior : MonoBehaviour
{
    public GameObject Gem;
    public GameObject colliderInMid;

    public Vector3 targetScale = new Vector3(2f, 2f, 2f);

    // if the collider has already been diabled for 1 time,
    // when the player entered for the second time shou
    private bool diabledCollider = false;

    public GameObject gemToPlace;

    // Start is called before the first frame update
    void Start()
    {
        Gem.SetActive(false);
        GemBehavior gb = Gem.GetComponent<GemBehavior>();

        Debug.Log((gb.GetStatusOfGem()));
        // set Gem as active if player already handin potion in lava region
        if (gb.GetStatusOfGem() && !diabledCollider)
        {
            Gem.SetActive(true);
        }
    }


    public void MoveGem()
    {
        diabledCollider = true;

        // Disable the collider in the middle
        colliderInMid.gameObject.SetActive(false);

        // Start the coroutine to move and scale the gem
        StartCoroutine(MoveGemToPositionAndScale(gemToPlace.transform.position, targetScale, 2f)); // Moves and scales over 2 seconds
    }

    private IEnumerator MoveGemToPositionAndScale(Vector3 targetPosition, Vector3 targetScale, float duration)
    {
        Vector3 startPosition = Gem.transform.position;
        Vector3 startScale = Gem.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the interpolation factor
            float t = elapsedTime / duration;

            // Smoothly move the gem towards the target position
            Gem.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Smoothly scale the gem up towards the target scale
            Gem.transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        // Ensure the final position and scale are accurate
        Gem.transform.position = targetPosition;
        Gem.transform.localScale = targetScale;
    }
}
