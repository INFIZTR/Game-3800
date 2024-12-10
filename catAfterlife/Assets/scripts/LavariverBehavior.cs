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

        StartCoroutine(MoveGemToPositionAndScale(gemToPlace.transform.position, targetScale, 2f)); 
    }

    private IEnumerator MoveGemToPositionAndScale(Vector3 targetPosition, Vector3 targetScale, float duration)
    {
        Vector3 startPosition = Gem.transform.position;
        Vector3 startScale = Gem.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / duration;

            Gem.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            Gem.transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        Gem.transform.position = targetPosition;
        Gem.transform.localScale = targetScale;

        DisableEverythingExceptSpriteRenderer(Gem);

    }

    private void DisableEverythingExceptSpriteRenderer(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();

        foreach (Component component in components)
        {
            if (component is SpriteRenderer)
                continue;

            if (component is Behaviour behaviour)
            {
                behaviour.enabled = false;
            }
            else if (component is Collider collider)
            {
                collider.enabled = false;
            }
        }
    }

}
