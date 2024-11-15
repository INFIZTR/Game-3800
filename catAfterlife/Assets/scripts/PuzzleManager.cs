using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public int nextLevel = 2;
    public bool loadRewardPanel = false;
    public bool destroyWall = false;

    public void NextLevel()
    {
        if (loadRewardPanel)
        {
            LevelManager.displayRewardPanel = true;
        }
        if (destroyWall)
        {
            LevelManager.doDestoryWall = true;
        }
        SceneManager.LoadScene(nextLevel);
    }

    public void ReloadCurrentLevel()
    {
        // Get the active scene (current level) and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
