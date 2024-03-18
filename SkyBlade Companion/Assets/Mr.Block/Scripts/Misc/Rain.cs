using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private AudioClip _rainSoundClip;
    [SerializeField, Range(0f, 1f)] private float _rainSoundVolume = 1f;

    private AudioSource _rainAudioSource;

    private void Start()
    {
        RainAudioControls();
        _rainAudioSource.Play();
    }

    private void RainAudioControls()
    {
        _rainAudioSource = gameObject.AddComponent<AudioSource>();
        _rainAudioSource.clip = _rainSoundClip;
        _rainAudioSource.loop = true;
        _rainAudioSource.volume = _rainSoundVolume;
    }
}
