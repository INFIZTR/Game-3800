using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject loadingScreen;

    private void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void SwitchScene()
    {
        StartCoroutine(LoadNextScene("1_spawn"));
    }


    private IEnumerator LoadNextScene(string nextScene)
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // load the next scene asynchronously
        UnityEngine.AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        // wait until the scene has fully loaded
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
