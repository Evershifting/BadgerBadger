using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _popSource, _musicSource;

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void Pop()
    {
        _popSource.Play();
    }

    public void Mute()
    {
        _popSource.mute = !_popSource.mute;
        _musicSource.mute = !_musicSource.mute;
    }
}
