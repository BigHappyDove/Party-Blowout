using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    private Player _player;
    private PhotonView _pv;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private GameObject healthLogo;

    void Start()
    {
        _pv = GetComponent<PhotonView>();
        if (!_pv.IsMine)
        {
            Destroy(healthUI.transform.parent.gameObject);
            Destroy(this);
            return;
        }
        _player = GetComponent<Player>();
        UpdateHealth(); // We really don't care about the args
        Gamemode.onTakeDamageHook += UpdateHealth;
        // We hook this function to the event, so we don't have to update the player's ui every fucking frame
    }

    private void OnDestroy()
    {
        if(_pv.IsMine)
            Gamemode.onTakeDamageHook -= UpdateHealth;
    }

    /// <summary>
    /// Update the health on the player's ui
    /// </summary>
    /// <param name="ply">Useless arg</param>
    /// <param name="obj">Useless arg</param>
    void UpdateHealth(AliveEntity ply = null, object obj = null)
    {
        if (!(_player is null) && !(healthUI is null))
        {
            healthUI.SetText(_player.health.ToString(CultureInfo.CurrentCulture));
            ChangeColorHealth(_player.health);
        }
    }

    /// <summary>
    /// Update the color of the health logo. 100 = Green, 0 = Red
    /// </summary>
    /// <param name="playerHealth">The health of the player</param>
    void ChangeColorHealth(float playerHealth)
    {
        if(healthLogo is null) return;
        byte converted = (byte) Math.Min(Math.Max(playerHealth / 100 * 255, 0), 255); // 0 <= converted <= 255
        foreach (Transform t in healthLogo.transform)
        {
            Image i = t.gameObject.GetComponent<Image>();
            if (!(i is null)) i.color = new Color32((byte) (255 - converted), converted, 0, 255);
        }
    }
}
