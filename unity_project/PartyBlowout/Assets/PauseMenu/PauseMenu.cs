using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PhotonView _pv;
    public bool GameIsPaused = false; //to see if the game is already on pause
    public GameObject pausemenuUI;

    void Start()
    {
        _pv = GetComponent<PhotonView>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc is the key to press to access this menu
        {
            if (GameIsPaused)
                Resume(); // to leave the pause menu
            else
            {
                Cursor.visible = true;
                Pause(); // to access the pause menu
            }
        }
    }

    public void Resume()
    {
        GameIsPaused = false; //to reset the bool
        pausemenuUI.SetActive(false); // stop showing the menu UI
    }

    public void Pause()
    {
        GameIsPaused = true; // set the bool
        pausemenuUI.SetActive(true); // to show the UI
    }

    public void LoadMenu()
    {
        if(!_pv.IsMine) return;
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        if (!_pv.IsMine) return;
        Debug.Log("Player quit the game");
        Application.Quit();
    }

}
