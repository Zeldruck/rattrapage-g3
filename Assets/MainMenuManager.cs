using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public string nameScene;

    public void Play()
    {
        SceneManager.LoadScene(nameScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
