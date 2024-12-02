using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoBehavior : MonoBehaviour
{
    public bool display = false;
    public GameObject player;
    public GameObject levelManager;
    float activeTime = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (levelManager == null)
        {
            levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        }

        var lm = levelManager.GetComponent<LevelManager>();
        if (lm.PuzzleInvoked(SceneManager.GetActiveScene().buildIndex))
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            var lm = levelManager.GetComponent<LevelManager>();
            int buildindex = SceneManager.GetActiveScene().buildIndex;
            if (display && !lm.PuzzleInvoked(buildindex))
            {
                gameObject.SetActive(true);
                var ps = player.GetComponent<PlayerMovementGrids>();
                ps.LockPlayer();

                // Start the coroutine to wait for the scene to load and then count down
                Debug.Log(activeTime);

                lm.AddNewIndex(buildindex);

                StartCoroutine(WaitForSceneAndSetInactive(activeTime));
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        var ps = player.GetComponent<PlayerMovementGrids>();
        ps.UnlockPlayer();
    }

    private IEnumerator WaitForSceneAndSetInactive(float delay)
    {

        // Now start the countdown
        yield return new WaitForSeconds(delay);

        // Set the object inactive and unlock the player
        Hide();
    }
}
