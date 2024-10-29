using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public int nextLevel = 2;
    public bool loadRewardPanel = false;
    public void NextLevel()
    {
        if (loadRewardPanel)
        {
            LevelManager.displayRewardPanel = true;
        }
        SceneManager.LoadScene(nextLevel);

    }
}
