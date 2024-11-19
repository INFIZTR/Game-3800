using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBehavior : MonoBehaviour
{
    public bool display = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (display)
        {
            gameObject.SetActive(true);
            var ps = player.GetComponent<PlayerMovementGrids>();
            ps.LockPlayer();

            // Start the coroutine to hide the object after 4 seconds
            StartCoroutine(SetInactiveAfterDelay(4f));
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        var ps = player.GetComponent<PlayerMovementGrids>();
        ps.UnlockPlayer();
    }

    private IEnumerator SetInactiveAfterDelay(float delay)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delay);

        // Set the object inactive and unlock the player
        Hide();
    }
}
