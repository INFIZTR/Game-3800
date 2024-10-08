using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene(3);
    }
}
