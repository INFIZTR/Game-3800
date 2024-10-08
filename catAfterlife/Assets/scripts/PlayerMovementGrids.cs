using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementGrids : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private UnityEngine.Vector2 movement;

    bool moveForFirstTime = true;

    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap playerTracemap;
    public TileBase tileBase;

    private List<Vector3Int> initiatedTilePos;

    public int levelFinishTile = 34;
    int countNewTile = 0;
    public GameObject puzzleManager;
    bool loadNextLevel = false;

    public GameObject Canvas;


    private void Start()
    {
        moveForFirstTime = true;
        initiatedTilePos = new List<Vector3Int>();
        loadNextLevel = false;
        Canvas.SetActive(false);
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
        if (movement != UnityEngine.Vector2.zero)
        {
            if (CanMove(movement))
            {
                if (moveForFirstTime)
                {
                    Vector3Int init = groundTilemap.WorldToCell(transform.position);
                    playerTracemap.SetTile(init, tileBase);
                    initiatedTilePos.Add(init);
                    countNewTile++;
                    moveForFirstTime = false;
                }
                UnityEngine.Vector2 moveAmount = movement.normalized * 2;
                Vector3Int vi = groundTilemap.WorldToCell(transform.position += (UnityEngine.Vector3)moveAmount);
                // modify player's movement and setup new tile
                if (!initiatedTilePos.Contains(vi))
                {
                    playerTracemap.SetTile(vi, tileBase);
                    initiatedTilePos.Add(vi);
                    countNewTile++;
                }


                for (int i = 0; i < 7; i++)
                {
                    if (CanMove(movement))
                    {
                        vi = groundTilemap.WorldToCell(transform.position += (UnityEngine.Vector3)moveAmount);
                        if (!initiatedTilePos.Contains(vi))
                        {
                            playerTracemap.SetTile(vi, tileBase);
                            initiatedTilePos.Add(vi);
                            countNewTile++;
                        }
                    }
                }

                Debug.Log(countNewTile);
            }
        }

        if (countNewTile >= levelFinishTile && !loadNextLevel)
        {
            StartCoroutine(DelayNextLevel(2));
            loadNextLevel = true;
            Canvas.SetActive(true);
        }
        
    }

    IEnumerator DelayNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        var pm = puzzleManager.GetComponent<PuzzleManager>();
        pm.NextLevel();
    }

    private bool CanMove(UnityEngine.Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + 2*(Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
        {
            return false;
        }
        return true;
    }
    


}
