using System;
using Photon.Pun;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Serializable]
    public struct Struct_sounds
    {
        public Sound sound;
        public float maxDistance;
    }

    public Struct_sounds[] sounds;
    private PhotonView _pv;
    void Awake()
    {
        _pv = GetComponent<PhotonView>();
        foreach (Struct_sounds s in sounds)
        {
            s.sound.source = gameObject.AddComponent<AudioSource>();
            s.sound.source.clip = s.sound.clip;
            s.sound.source.volume = s.sound.volume;
            s.sound.source.pitch = s.sound.pitch == 0 ? 1 : s.sound.pitch;
            s.sound.source.loop = s.sound.loop;
            s.sound.source.maxDistance = s.maxDistance;
            s.sound.source.minDistance = 1;
            s.sound.source.spatialBlend = 1; // = 3D mode
            s.sound.source.rolloffMode = AudioRolloffMode.Logarithmic;
            s.sound.source.spatialize = true;
        }
    }

    private void OnDestroy()
    {
        foreach (Struct_sounds s in sounds)
            s.sound.source.Stop();
    }

    private Struct_sounds? FindSound(string name)
    {
        Struct_sounds sound = Array.Find(sounds, s => s.sound.name == name);
        if (sound.sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }
        return sound;
    }
    public void Play(string name, bool oneShot = false)
    {
        if(_pv == null || !_pv.IsMine) return;
        _pv.RPC("RPC_Play", RpcTarget.All, name, oneShot);
    }

    public void Stop(string name)
    {
        if(_pv == null || !_pv.IsMine) return;
        _pv.RPC("RPC_Stop", RpcTarget.All, name);
    }

    [PunRPC]
    public void RPC_Play(string name, bool oneShot = false)
    {
        AudioSource s = FindSound(name)?.sound.source;
        if(!s) return;
        if(oneShot)
            s.PlayOneShot(s.clip);
        else
            s.Play();
    }

    [PunRPC]
    public void RPC_Stop(string name)
    {
        AudioSource s = FindSound(name)?.sound.source;
        if(!s) return;
        s.Stop();
    }

    // public void Pitch(string name, float value) => FindSound(name)?.sound.pitch = value;
}
