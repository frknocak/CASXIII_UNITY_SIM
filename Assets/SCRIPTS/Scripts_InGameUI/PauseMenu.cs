using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public int udpPort;
    public GameObject pauseMenuUI;
    public GameObject InGameSettingsMenu;
    public GameObject AdvancedPPPSettingMenu;
    public TextMeshProUGUI currentMissionText;
    public TextMeshProUGUI udpPortNumber;
    private string sceneName;

    void Update()
    {
        udpPort= PlayerPrefs.GetInt("udpPort");
        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
        currentMissionText.text = "Current Mission is " + sceneName;
        udpPortNumber.text = "UDP Port Number: " + udpPort;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        InGameSettingsMenu.SetActive(false);
        AdvancedPPPSettingMenu.SetActive(false);

        Time.timeScale = 1f;
        gameIsPaused = false;

    }
    public void Restart(){
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1f;
        gameIsPaused = false;


    
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("MissionSelector");
        Resume();
    }
    public void QuitButton(){
        Application.Quit();
    }
}
