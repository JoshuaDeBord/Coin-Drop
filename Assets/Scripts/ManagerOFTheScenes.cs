using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerOfTheScenes : MonoBehaviour
{
    public string sceneName;
   

    public void SceneChange()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
