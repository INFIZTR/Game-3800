using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("maping adjust level with numbers")]
    public int left = 0;
    public int top = 0;
    public int right = 0;
    public int bottom = 0;

    // determine if display reward panel at start of the scene
    public static bool displayRewardPanel = false;
    public static bool doDestoryWall = false;

    public GameObject[] destroiableWalls = {};
    public GameObject blockDestroyedText;

    public GameObject rewardPanel;

    public static bool invokeSceneForFirstTime = true;

    private void Start()
    {
        if (invokeSceneForFirstTime && blockDestroyedText != null)
        {
            blockDestroyedText.SetActive(false);
        }

        if (!doDestoryWall && invokeSceneForFirstTime && destroiableWalls.Length > 0)
        {
            foreach (GameObject wall in destroiableWalls)
            {
                wall.SetActive(true);
            }

            invokeSceneForFirstTime =false;
        }
    }

    private void Awake()
    {
        if (rewardPanel == null)
        {
            rewardPanel = GameObject.FindGameObjectWithTag("RewardPanel");
        }

        if (rewardPanel != null)
        {
            if (displayRewardPanel)
            {
                // display reward panel
                rewardPanel.SetActive(true);
                displayRewardPanel = false;

                
            }
            else
            {
                rewardPanel.SetActive(false);
            }
        }

        if (destroiableWalls != null)
        {
            if (doDestoryWall)
            {
                if (destroiableWalls.Length > 0)
                {
                    foreach (GameObject wall in destroiableWalls)
                    {
                        wall.SetActive(false);
                    }
                }
            } 
        }
       
    }

    // based on the input determine where to switch levels
    public void LevelSwitch(int direction)
    {
        switch (direction)
        {
            // case 0 represent top
            case 0:
                SceneManager.LoadScene(top);
                break;
            // case 1 represent left
            case 1:
                SceneManager.LoadScene(left);
                break;
            // case 2 represent bottom
            case 2:
                SceneManager.LoadScene(bottom);
                break;
            case 3:
                SceneManager.LoadScene(right);
                break;
        }
    }


}
