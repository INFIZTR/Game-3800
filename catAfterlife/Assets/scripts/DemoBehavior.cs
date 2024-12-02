using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoBehavior : MonoBehaviour
{
    public bool display = false;
    public GameObject player;
    float activeTime = 1.5f;
    bool invoked = false;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
       
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            if (display && !invoked)
            {
                invoked = true;
                gameObject.SetActive(true);
                var ps = player.GetComponent<PlayerMovementGrids>();
                ps.LockPlayer();

                // Start the coroutine to wait for the scene to load and then count down
                Debug.Log(activeTime);

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
