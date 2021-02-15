using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class updateNickname : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;
    void OnEnable()
    {
        _inputField.text = PhotonNetwork.NickName;
    }

    public void updateNickName()
    {
        PhotonNetwork.NickName = _inputField.text;
        DebugTools.PrintOnGUI($"Nickname is now {PhotonNetwork.NickName}");
    }
}