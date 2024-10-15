using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class PlayerPushBoxMov : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private UnityEngine.Vector2 movement;

    bool moveForFirstTime = true;

    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap boxTilemap;
    public Tile boxTile;


    private List<Vector3Int> initiatedTilePos;

    public int levelFinishTile = 34;
    int countNewTile = 0;
    public GameObject puzzleManager;
    bool loadNextLevel = false;

    public GameObject GameOver;
    public int totalSteps = 5;
    private int currentStep = 0;

    public Vector3Int winPos;

    bool moving = false;


    private void Start()
    {
        moveForFirstTime = true;
        initiatedTilePos = new List<Vector3Int>();
        loadNextLevel = false;
        GameOver.SetActive(false);

        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player (WASD or arrow keys)
        // only takes in 1 direction at a time
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // if user pressed both x and y
        if (movement.x != 0 & movement.y != 0)
        {
            // Prioritize horizontal movement
            movement.y = 0;
        }

    }

    void FixedUpdate()
    {
        // Move the player
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement != Vector2.zero)
        {
            // remember the movement
            if (CanMove(movement) && !moving)
            {
                Vector2 store = movement;
                currentStep++;

                // if player is moving towards box, and there's space behind box
                Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + 2 * (Vector3)movement.normalized);
                Vector3Int boxToPut = groundTilemap.WorldToCell(transform.position + 4 * (Vector3)movement.normalized);
                if (boxTilemap.HasTile(gridPosition))
                {
                    boxTilemap.SetTile(gridPosition, null);
                    boxTilemap.SetTile(boxToPut, boxTile);
                }

                Vector2 moveAmount = store.normalized * 2;
                transform.position += (Vector3)moveAmount;
                Debug.Log("current step: "+ groundTilemap.WorldToCell(transform.position));

                movement = Vector2.zero;
                Debug.Log(countNewTile);
            }
        }

        Vector3Int currentPos = groundTilemap.WorldToCell(transform.position);
        if (currentPos == winPos && !loadNextLevel)
        {
            StartCoroutine(DelayNextLevel(2));
            loadNextLevel = true;
            GameOver.SetActive(true);
        }
    }

    IEnumerator DelayNextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        var pm = puzzleManager.GetComponent<PuzzleManager>();
        pm.NextLevel();
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + 2 * (Vector3)direction);
        Vector3Int boxToPut = groundTilemap.WorldToCell(transform.position + 4 * (Vector3)movement.normalized);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition) 
            || (boxTilemap.HasTile(gridPosition) && (collisionTilemap.HasTile(boxToPut) || boxTilemap.HasTile(boxToPut))))
        {
            return false;
        }
        return true;
    }



}
