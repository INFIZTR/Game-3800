using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    public Animator animator;

    private bool ableToMove = true;

    private void Start()
    {
        ableToMove = true;
        if (animator ==  null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player (WASD or arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // if moving right
        if (movement.x > 0)
        {
            animator.SetInteger("behavior", 4);
        }
        // if moving left
        else if (movement.x < 0) 
        {
            animator.SetInteger("behavior", 3);
        }
        else if (movement.y > 0) 
        {
            animator.SetInteger("behavior", 1);
        }
        else if (movement.y < 0)
        {
            animator.SetInteger("behavior", 2);
        }
        else
        {
            animator.SetInteger("behavior", 0);
        }
    }

    void FixedUpdate()
    {
        if (ableToMove)
        {
            // Move the player
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // ignore inputs from user
    public void LockMovement()
    {
        ableToMove = false;
    }

    public void UnlockMovement()
    {
        ableToMove = true;
    }
}
