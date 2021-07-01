using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    private bool isPause = false;
    private bool isLose = false;

    public GameObject pauseMenu;
    public GameObject loseMenu;
    [Space]
    public AudioSource ascThunder;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        loseMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isLose)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (!isPause)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            isPause = true;

            if (ascThunder != null && ascThunder.isPlaying)
            {
                ascThunder.Pause();
            }
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            isPause = false;

            if (ascThunder != null)
            {
                ascThunder.UnPause();
            }
        }
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        isLose = true;
        loseMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Retry(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
