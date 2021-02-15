using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void CreateMultiplayerGame()
    {
        string name = RoomMatch.TryCreateRoom();
        RoomMatch.TryJoinRoom(name);
        //TODO: handle when we can't join/create room
    }
    

    public void QuitGame()
    {
        Debug.Log("Player quit the game");
        Application.Quit();
    }
}
