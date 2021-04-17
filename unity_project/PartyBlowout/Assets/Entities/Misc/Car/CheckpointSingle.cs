using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckpointSingle : MonoBehaviour
{
    private PhotonView _pv;
    
    private TrackCheckpoints _trackCheckpoints;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        Hide();
        _pv = GetComponent<PhotonView>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_pv.IsMine)
        {
            return;
        }
        if (other.TryGetComponent(out Player player))
        {
            _trackCheckpoints.PlayerThroughCheckpoint(this, other.transform);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints)
    {
        this._trackCheckpoints = trackCheckpoints;
    }

    public void Hide()
    {
        _meshRenderer.enabled = false;
    }

    public void Show()
    {
        _meshRenderer.enabled = true;
    }
}
