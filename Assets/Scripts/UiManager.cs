using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endingMenu;
    [SerializeField] Image UnstableShroomImage_P1;
    [SerializeField] Image UnstableShroomImage_P2;
    [SerializeField] GameObject EndingTitle;
    [SerializeField] GameObject GameUi;

    private bool isPaused;

    private void Start()
    {
        if(UnstableShroomImage_P1 == null)
        {
            Debug.LogError("UnstableShroomImage_P1 is null on UiManager");
        }
        if (UnstableShroomImage_P2 == null)
        {
            Debug.LogError("UnstableShroomImage_P2 is null on UiManager");
        }
        if (GameUi == null)
        {
            Debug.LogError("GameUi is null on UiManager");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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
                        Screen.fullScreen = false;
        #endif
    }

    public void PauseGame()
    {
        Debug.Log("Pause Game");
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameUi.SetActive(false);
    }

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameUi.SetActive(true);
    }

    public void ShowEndingMenu(string losingPlayerType)
    {
        if (EndingTitle != null)
        {
            TextMeshProUGUI text = EndingTitle.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                string WinningPlayerString = "";
                if(losingPlayerType == "P1")
                {
                    WinningPlayerString = "2";
                } else if (losingPlayerType == "P2")
                {
                    WinningPlayerString = "1";
                }
                text.text = "WINNER: Player " + WinningPlayerString;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI text is null on EndingTitle GameObject");
            }
        } else
        {
            Debug.LogError("EndingTitle null");
        }
        endingMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetCanPlaceShroom(bool canPlaceShroom, string playerType)
    {
        Debug.Log("SetCanPlaceShroom");
        float alpha = 1f;
        if(!canPlaceShroom)
        {
            alpha = 0.3f;
        }
        if(playerType != null)
        {
            Image UnstableShroomImage = null;
            if(playerType == "P1")
            {
                UnstableShroomImage = UnstableShroomImage_P1;
            } else if (playerType == "P2")
            {
                UnstableShroomImage = UnstableShroomImage_P2;
            } else
            {
                Debug.LogError("Unsupported playerType in SetCanPlaceShroom");
            }
            if (UnstableShroomImage == null)
            {
                Debug.LogError("UnstableShroomImage is null");
            }
            else
            {
                UnstableShroomImage.color = new Color(UnstableShroomImage.color.r, UnstableShroomImage.color.g, UnstableShroomImage.color.b, alpha);
            }
        } else
        {
            Debug.LogError("playerType is null in SetCanPlaceShroom");
        }
        
    }
}
