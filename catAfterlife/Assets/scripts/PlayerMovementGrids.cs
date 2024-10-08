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

    bool moveForFirstTime = true;

    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap playerTracemap;
    public TileBase tileBase;

    int countNewTile = 0;
    public int levelFinishTile = 36;
    public GameObject puzzleManager;


    private void Start()
    {
        moveForFirstTime = true;
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
        // Move the player
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement != Vector2.zero)
        {
            if (CanMove(movement))
            {
                if (moveForFirstTime)
                {
                    Vector3Int init = groundTilemap.WorldToCell(transform.position);
                    playerTracemap.SetTile(init, tileBase);
                }
                Vector2 moveAmount = movement.normalized * 2;
                Vector3Int vi = groundTilemap.WorldToCell(transform.position += (Vector3)moveAmount);
                // modify player's movement and setup new tile
                playerTracemap.SetTile(vi, tileBase);
                countNewTile++;

                for (int i = 0; i < 7; i++)
                {
                    if (CanMove(movement))
                    {
                        vi = groundTilemap.WorldToCell(transform.position += (Vector3)moveAmount);
                        playerTracemap.SetTile(vi, tileBase);
                        countNewTile++;
                    }
                }
            }
        }

        if (countNewTile >= levelFinishTile)
        {
            var pm = puzzleManager.GetComponent<PuzzleManager>();
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
