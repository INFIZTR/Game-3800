using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public int nextLevel = 2;
    public bool loadRewardPanel = false;
    public bool destroyWall = false;

    //public int previousLevelIndex = 0;

    // screen to display when loading next scene
    public GameObject loadingScreen;

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
        StartCoroutine(LoadNextScene(nextLevel));
    }

    public void ReloadCurrentLevel()
    {
        // Get the active scene (current level) and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadNextScene(nextLevel));

    }

    private IEnumerator LoadNextScene(int nextSceneIndex)
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // load the next scene asynchronously
        UnityEngine.AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneIndex);

        // wait until the scene has fully loaded
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
