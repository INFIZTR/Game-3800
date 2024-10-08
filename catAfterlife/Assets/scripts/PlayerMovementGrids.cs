using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementGrids : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap playerTracemap;
    public TileBase tileBase;


    // Update is called once per frame
    void Update()
    {
        // Get input from the player (WASD or arrow keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Move the player
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (CanMove(movement))
        {
            Vector2 moveAmount = movement.normalized * 2;
            Vector3Int vi = groundTilemap.WorldToCell(transform.position += (Vector3)moveAmount);
            // modify player's movement and setup new tile
            playerTracemap.SetTile(vi, tileBase);

            for (int i = 0; i < 7; i++) {
                if (CanMove(movement))
                {
                    vi = groundTilemap.WorldToCell(transform.position += (Vector3)moveAmount);
                    playerTracemap.SetTile(vi, tileBase);
                }
            }
        }
    }

    
    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + 2*(Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
        {
            return false;
        }
        return true;
    }
    


}
