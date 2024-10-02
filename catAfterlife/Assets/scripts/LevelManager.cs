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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
            default:
                SceneManager.LoadScene(right);
                break;
        }
    }
}
