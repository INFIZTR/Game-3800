using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;


public class PlayerMovementGrids : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    bool moveForFirstTime = true;

    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap playerTracemap;
    public TileBase tile_front;
    public TileBase tile_back;
    public TileBase tile_left;
    public TileBase tile_right;


    private HashSet<Vector3Int> initiatedTilePos;

    public int levelFinishTile = 34;
    int countNewTile = 0;
    public GameObject puzzleManager;
    bool loadNextLevel = false;

    public GameObject GameOver;
    public int totalSteps = 5;
    private  int currentStep = 0;
    public GameObject steps;
    TextMeshProUGUI stepsText;

    bool moving = false;


    private void Start()
    {
        moveForFirstTime = true;
        initiatedTilePos = new HashSet<Vector3Int>();
        loadNextLevel = false;
        GameOver.SetActive(false);
        stepsText = steps.GetComponent<TextMeshProUGUI>();

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
        if (movement.x != 0 &  movement.y != 0)
        {
            // Prioritize horizontal movement
            movement.y = 0;
        }
        

        if (!loadNextLevel)
        {
            string newText = "Remaining Steps: " + (totalSteps - currentStep);
            stepsText.SetText(newText);
        }

        if (totalSteps - currentStep <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void setupTile(Vector3Int pos, Vector2 dirction)
    {
        if (dirction.x > 0)
        {
            playerTracemap.SetTile(pos, tile_right);
        }
        else if (dirction.x < 0)
        {
            playerTracemap.SetTile(pos, tile_left);
        }
        else if (dirction.y < 0)
        {
            playerTracemap.SetTile(pos, tile_back);
        }
        else
        {
            playerTracemap.SetTile(pos, tile_front);
        }

        initiatedTilePos.Add(pos);
        countNewTile++;
    }
    void FixedUpdate()
    {
        // only allows if game not over
        if (currentStep<totalSteps && !loadNextLevel)
        {
            // Move the player
            //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            if (movement != Vector2.zero)
            {
                // remember the movement
                if (CanMove(movement) && !moving)
                {
                    Vector2 store = movement;
                    moving = true;
                    currentStep++;
                    if (moveForFirstTime)
                    {
                        Vector3Int init = groundTilemap.WorldToCell(transform.position);
                        //playerTracemap.SetTile(init, tileBase);
                        //initiatedTilePos.Add(init);
                        //countNewTile++;
                        setupTile(init, store);
                        moveForFirstTime = false;
                    }
                    Vector2 moveAmount = store.normalized * 2;
                    Vector3Int vi = groundTilemap.WorldToCell(transform.position += (UnityEngine.Vector3)moveAmount);
                    // modify player's movement and setup new tile
                    if (!initiatedTilePos.Contains(vi))
                    {
                        setupTile(vi, store);
                    }


                    for (int i = 0; i < 7; i++)
                    {
                        if (CanMove(store))
                        {
                            vi = groundTilemap.WorldToCell(transform.position += (UnityEngine.Vector3)moveAmount);
                            if (!initiatedTilePos.Contains(vi))
                            {
                                setupTile(vi, store);
                            }
                        }
                    }

                    moving = false;

                }
            }

            UnityEngine.Debug.Log(countNewTile);
            if (countNewTile >= levelFinishTile && !loadNextLevel)
            {
                StartCoroutine(DelayNextLevel(2));
                loadNextLevel = true;
                GameOver.SetActive(true);
            }
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
        //if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
        if (!groundTilemap.HasTile(gridPosition))
        {
            return false;
        }
        return true;
    }
    

    /*
    private bool CanMove(Vector2 direction)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();  // Start the stopwatch

        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + 2 * (Vector3)direction);
        bool canMove = !groundTilemap.HasTile(gridPosition) || !collisionTilemap.HasTile(gridPosition);

        stopwatch.Stop();  // Stop the stopwatch
        UnityEngine.Debug.Log("CanMove runtime: " + stopwatch.ElapsedMilliseconds + " ms");

        return canMove;
    }
    */



}
