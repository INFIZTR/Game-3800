using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Tooltip("left,right,top,bottom")]
    public Collider2D[] colliders;
    Collider2D leftCollider;
    Collider2D rightCollider;
    Collider2D topCollider;
    Collider2D bottomCollider;
    // Start is called before the first frame update
    void Start()
    {
        leftCollider = colliders[0];
        rightCollider = colliders[1];
        topCollider = colliders[2];
        bottomCollider = colliders[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if player collidea with boarder
        if (collision.collider.tag == "player")
        {

        }
    }
}
