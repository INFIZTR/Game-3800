using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private bool ableToMove = true;

    private void Start()
    {
        ableToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player (WASD or arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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
