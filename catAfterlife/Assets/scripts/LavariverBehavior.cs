using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavariverBehavior : MonoBehaviour
{
    public GameObject Gem;

    // Start is called before the first frame update
    void Start()
    {
        Gem.SetActive(false);
        GemBehavior gb = Gem.GetComponent<GemBehavior>();

        Debug.Log((gb.GetStatusOfGem()));
        // set Gem as active if player already handin potion in lava region
        if (gb.GetStatusOfGem())
        {
            Gem.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
