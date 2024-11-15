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
            Debug.Log(12222222222222222222);
            LevelManager.doDestoryWall = true;
        }
        SceneManager.LoadScene(nextLevel);

    }
}
