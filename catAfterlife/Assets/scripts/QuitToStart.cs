using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToStart : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
