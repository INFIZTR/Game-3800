using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavariverBehavior : MonoBehaviour
{
    public GameObject Gem;
    public GameObject colliderInMid;

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

    // move gem on the river
    public void MoveGem()
    {
        diabledCollider = true;
        // disable the collider in the mid
        colliderInMid.gameObject.SetActive(false);

        // todo:
    }
}
