using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TransitionManager : MonoBehaviour
{
    private VideoPlayer[] _videoPlayers;
    private AudioSource _audio;
    void Awake()
    {
        _videoPlayers = GetComponentsInChildren<VideoPlayer>();
        _audio = GetComponent<AudioSource>();
        foreach (VideoPlayer vp in _videoPlayers) vp.gameObject.SetActive(false);
    }

    public void PlaySelected(Gamemode.CurrentGamemode selectedGm)
    {
        foreach (VideoPlayer vp in _videoPlayers)
            if (vp.gameObject.name == selectedGm.ToString())
                PlayVideo(vp);
    }

    private void PlayVideo(VideoPlayer vp)
    {
        vp.gameObject.SetActive(true);
        vp.targetCamera = FindObjectOfType<Camera>();
        vp.Play();
        if(_audio)
            _audio.PlayOneShot(_audio.clip);
    }
}
