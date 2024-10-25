using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class PlayerPushBoxMov : MonoBehaviour
{
    private Vector2 movement;

    bool moveForFirstTime = true;

    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap boxTilemap;
    public Tile boxTile;


    private List<Vector3Int> initiatedTilePos;

    int countNewTile = 0;
    public GameObject puzzleManager;
    bool loadNextLevel = false;

    public GameObject GameOver;
    public int totalSteps = 5;

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
        // Reset movement every frame
        movement = Vector2.zero;

        // Check for key presses and update movement
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.y = -1;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.x = 1;
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

                // if player is moving towards box, and there's space behind box
                Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + 2 * (Vector3)store.normalized);
                Vector3Int boxToPut = groundTilemap.WorldToCell(transform.position + 4 * (Vector3)store.normalized);
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
        Debug.Log(currentPos);
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
        Vector3Int boxToPut = groundTilemap.WorldToCell(transform.position + 4 * (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition) 
            || (boxTilemap.HasTile(gridPosition) && (collisionTilemap.HasTile(boxToPut) || boxTilemap.HasTile(boxToPut))))
        {
            return false;
        }
        return true;
    }

    // handled by start button
    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }


}
