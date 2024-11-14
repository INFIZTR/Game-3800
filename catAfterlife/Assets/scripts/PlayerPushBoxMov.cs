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

    Vector2 lastCalledMovement = Vector2.zero;
    bool updateTileLocker = false;


    private void Start()
    {
        moveForFirstTime = true;
        initiatedTilePos = new List<Vector3Int>();
        loadNextLevel = false;
        GameOver.SetActive(false);
        updateTileLocker = false;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!updateTileLocker)
        {
            // Get input from the player (WASD or arrow keys)
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Prioritize horizontal movement if both directions are pressed
            if (movement.x != 0 && movement.y != 0)
            {
                movement.y = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero && !updateTileLocker)
        {
            Debug.Log("Entering lock");

            updateTileLocker = true;

            if (CanMove(movement) && !moving)
            {
                currentStep++;
                StartCoroutine(MovePlayer(movement));
            }
        }

        // Check if the player reached the win position
        Vector3Int currentPos = groundTilemap.WorldToCell(transform.position);
        if (currentPos == winPos && !loadNextLevel)
        {
            StartCoroutine(DelayNextLevel(2));
            loadNextLevel = true;
            GameOver.SetActive(true);
        }
    }

    private IEnumerator MovePlayer(Vector2 direction)
    {
        moving = true;

        // Calculate the target position and move amount
        Vector2 moveAmount = direction.normalized * 2f;
        Vector3 targetPosition = transform.position + (Vector3)moveAmount;

        // Handle box movement logic if needed
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + 2 * (Vector3)direction.normalized);
        Vector3Int boxToPut = groundTilemap.WorldToCell(transform.position + 4 * (Vector3)direction.normalized);

        if (boxTilemap.HasTile(gridPosition))
        {
            boxTilemap.SetTile(gridPosition, null);
            boxTilemap.SetTile(boxToPut, boxTile);
        }

        // Smoothly move the player to the target position
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsed);
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        // Snap to final position and reset movement
        transform.position = targetPosition;
        movement = Vector2.zero;
        updateTileLocker = false;
        moving = false;
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