using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] Image UnstableShroomImage;

    private bool isPaused;

    private void Start()
    {
        if(UnstableShroomImage == null)
        {
            Debug.Log("UnstableShroomImage is null on UiManager");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif
        #if (UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
                            Application.Quit();
        #elif (UNITY_WEBGL)
                        SceneManager.LoadScene("MainMenu");
        #endif
    }

    public void PauseGame()
    {
        Debug.Log("Pause Game");
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetCanPlaceShroom(bool canPlaceShroom)
    {
        Debug.Log("SetCanPlaceShroom");
        float alpha = 1f;
        if(!canPlaceShroom)
        {
            alpha = 0.3f;
        }
        if (UnstableShroomImage == null)
        {
            Debug.LogError("UnstableShroomImage is null");
        }
        else
        {
            UnstableShroomImage.color = new Color(UnstableShroomImage.color.r, UnstableShroomImage.color.g, UnstableShroomImage.color.b, alpha);
        }
    }
}
