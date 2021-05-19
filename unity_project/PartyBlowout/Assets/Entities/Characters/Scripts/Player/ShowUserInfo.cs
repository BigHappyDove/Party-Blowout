using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using PhotonPlayer = Photon.Realtime.Player;

public class ShowUserInfo : MonoBehaviourPunCallbacks
{
    private TextMesh _textMesh;
    private Player _player;
    private PhotonView _pv;

    // Start is called before the first frame update
    void Awake()
    {
        _pv = GetComponent<PhotonView>();
        // if(_pv.IsMine) Destroy(gameObject);
        _player = GetComponentInParent<Player>();
        _textMesh = GetComponent<TextMesh>();
        CheckColor();
    }

    public override void OnPlayerPropertiesUpdate(PhotonPlayer targetPlayer, Hashtable changedProps)
    {
        CheckColor();
    }

    private void CheckColor()
    {
        if(_textMesh == null) return;
        Color color = default;
        if (_player != null)
        {
            switch (AliveEntity.GetTeam(_pv))
            {
                case Gamemode.PlayerTeam.Blue:
                    color = Color.blue;
                    break;
                case Gamemode.PlayerTeam.Red:
                    color = Color.red;
                    break;
            }
        }

        if (color != default)
            _textMesh.color = color;
    }


    // Update is called once per frame
    private void Update()
    {
        if(_textMesh == null) return;
        transform.LookAt(transform.position);
        _textMesh.text = _pv.Owner.NickName + "\n" + (_player != null ? ((int) _player.health) + "%" : "");
    }
}
