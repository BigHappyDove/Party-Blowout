using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    private Player _player;
    private PhotonView _pv;
    [SerializeField] private TextMeshProUGUI healthUI;

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
        print(_player.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(_player is null))
            healthUI.SetText(_player.health.ToString(CultureInfo.CurrentCulture));
    }
}
