using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; //to see if the game is already on pause

    public GameObject pausemenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc is the key to press to access this menu
        {
            if (GameIsPaused)
            {
                Resume(); // to leave the pause menu
            }
            else
            {
                Pause(); // to access the pause menu
            }
        }
    }

    public void Resume()
    {
        pausemenuUI.SetActive(false); // stop showing the menu UI
        Time.timeScale = 1f; //normal game speed
        GameIsPaused = false; //to reset the bool
    }

    void Pause()
    {
        pausemenuUI.SetActive(true); // to show the UI
        Time.timeScale = 0f; // freeze the game
        GameIsPaused = true; // set the bool
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Player quit the game");
        Application.Quit();
    }
    
}
