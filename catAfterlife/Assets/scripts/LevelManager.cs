using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    [Tooltip("maping adjust level with numbers")]
    public int left = 0;
    public int top = 0;
    public int right = 0;
    public int bottom = 0;

    // screen to display when loading next scene
    public GameObject loadingScreen;

    // determine if display reward panel at start of the scene
    public static bool displayRewardPanel = false;
    public static bool doDestoryWall = false;

    public GameObject[] destroiableWalls = {};
    public GameObject blockDestroyedText;

    public GameObject rewardPanel;

    // if the current scene will instantiate a list of objects
    public bool instantiateObjects = true;

    [Tooltip("list of positions to instantiate pickups")]
    public GameObject[] instantiate_Pickup_Position;

    public GameObject[] list_pickups;

    // a static list that stores the scene index that instantiated pickups
    private static List<int> list_instantiate_pickups = new List<int>();

    private static Dictionary<int, GameObject> tempParents = new Dictionary<int, GameObject>();


    public static bool invokeSceneForFirstTime = true;

    // store pickups
    // "static" to maintain the scope
    private static GameObject container;

    // store the index of puzzles that player has already entered.
    private static List<int> puzzlesThatHasTriggered = new List<int>();

    public GameObject fadingBlack;
    public GameObject gameEndScene;
    public float fadeDuration = 2f;
    public float delayBeforeSceneLoad = 3f;
    public bool gameOverTrigger = false;
    private void OnDestroy()
    {
        if (!instantiateObjects)
        {
            return;
        }
        string currentSceneIndexName = SceneManager.GetActiveScene().buildIndex.ToString();

        // Find the parent object with the same name as the current scene index
        GameObject temp_parent = GameObject.Find(currentSceneIndexName);

        if (temp_parent != null)
        {
            // Set the parent object inactive
            temp_parent.SetActive(false);
            Debug.Log($"Set {currentSceneIndexName} item container inactive.");
        }
        else
        {
            Debug.LogWarning($"No parent object found with the name {currentSceneIndexName}.");
        }
    }

    private void Start()
    {
        fadingBlack.SetActive(false);
        gameEndScene.SetActive(false);

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

        }

        invokeSceneForFirstTime = false;
    }

    private void Update()
    {
        if (gameOverTrigger)
        {
            GameEnd();
        }
    }

    private void Awake()
    {
        if (!instantiateObjects)
        {
            return;
        }


        if (container == null)
        {
            container = new GameObject("DontDestroyOnLoadContainer");
            container.tag = "pickUpContainer";
            container.AddComponent<DontDestroyOnLoadContainer>();
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // instantiate pickups if it is first time open the scene
        if (!tempParents.ContainsKey(currentSceneIndex))
        {
            int positionIndex = 0;
            GameObject temp_parent = new GameObject(currentSceneIndex.ToString());
            temp_parent.transform.SetParent(container.transform);
            foreach (GameObject it in list_pickups)
            {
                GameObject temp = Instantiate(it, instantiate_Pickup_Position[positionIndex].transform);
                temp.transform.SetParent(temp_parent.transform);
                positionIndex++;
            }

            //list_instantiate_pickups.Add(SceneManager.GetActiveScene().buildIndex);
            tempParents[currentSceneIndex] = temp_parent;
        }
        // else if there's already instantiated pickups
        else
        {
            // Reactivate the existing parent object for this scene
            GameObject temp_parent = tempParents[currentSceneIndex];

            if (temp_parent != null)
            {
                temp_parent.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Parent object for scene {currentSceneIndex} is missing.");
            }
        }

        if (rewardPanel == null)
        {
            rewardPanel = GameObject.FindGameObjectWithTag("RewardPanel");
        }

        if (rewardPanel != null)
        {
            if (displayRewardPanel)
            {
                var rp = rewardPanel.GetComponent<RewardPanel>();
                if (rp != null)
                {
                    if (!rp.alreadyInvoked)
                    {
                        // display reward panel
                        rewardPanel.SetActive(true);
                        displayRewardPanel = false;
                    }
                }

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
                StartCoroutine(LoadNextScene(top));
                break;
            // case 1 represent left
            case 1:
                StartCoroutine(LoadNextScene(left));
                break;
            // case 2 represent bottom
            case 2:
                StartCoroutine(LoadNextScene(bottom));
                break;
            case 3:
                StartCoroutine(LoadNextScene(right));
                break;
        }
    }

    private IEnumerator LoadNextScene(int nextSceneIndex)
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        // load the next scene asynchronously
        UnityEngine.AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneIndex);

        // wait until the scene has fully loaded
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    // given a puzzle index, determine if that puzzle has already been invoked before
    public bool PuzzleInvoked(int index)
    {
        return puzzlesThatHasTriggered.Contains(index);
    }

    // add new index to the puzzle index list
    public void AddNewIndex(int index)
    {
        puzzlesThatHasTriggered.Add(index);
    }


    public void GameEnd()
    {
        StartCoroutine(FadeInFadingBlack());
    }


    private IEnumerator FadeInFadingBlack()
    {
        // Ensure fadingBlack is active
        fadingBlack.SetActive(true);

        // Get the Image component of fadingBlack
        Image fadingBlackImage = fadingBlack.GetComponent<Image>();
        if (fadingBlackImage == null)
        {
            Debug.LogError("fadingBlack is missing an Image component.");
            yield break;
        }

        // Gradually fade in fadingBlack
        float elapsedTime = 0f;
        Color blackColor = fadingBlackImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadingBlackImage.color = new Color(blackColor.r, blackColor.g, blackColor.b, alpha);
            yield return null;
        }

        // Ensure fadingBlack is fully opaque
        fadingBlackImage.color = new Color(blackColor.r, blackColor.g, blackColor.b, 1f);

        // Start fading in the gameEndScene after fadingBlack is fully opaque
        StartCoroutine(FadeInGameEndScene());
    }

    private IEnumerator FadeInGameEndScene()
    {
        // Ensure gameEndScene is active
        gameEndScene.SetActive(true);

        // Get the Image component of gameEndScene
        Image gameEndImage = gameEndScene.GetComponent<Image>();
        if (gameEndImage == null)
        {
            Debug.LogError("gameEndScene is missing an Image component.");
            yield break;
        }

        // Gradually fade in gameEndScene
        float elapsedTime = 0f;
        Color endColor = gameEndImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            gameEndImage.color = new Color(endColor.r, endColor.g, endColor.b, alpha);
            yield return null;
        }

        // Ensure gameEndScene is fully opaque
        gameEndImage.color = new Color(endColor.r, endColor.g, endColor.b, 1f);

        yield return new WaitForSeconds(delayBeforeSceneLoad);


        SceneManager.LoadScene("StartScene");

    }


}
