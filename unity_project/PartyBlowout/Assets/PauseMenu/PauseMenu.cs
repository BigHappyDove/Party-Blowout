using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PhotonView _pv;
    public GameObject pausemenuUI;
    public bool GameIsPaused => pausemenuUI != null && pausemenuUI.activeSelf; //to see if the game is already on pause

    void Start()
    {
        _pv = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        if(_pv != null && !_pv.IsMine) return;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //esc is the key to press to access this menu
        {
            if (GameIsPaused)
                Resume(); // to leave the pause menu
            else
                Pause(); // to access the pause menu
        }

        if (GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Resume()
    {
        pausemenuUI.SetActive(false); // stop showing the menu UI
    }

    public void Pause()
    {
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
