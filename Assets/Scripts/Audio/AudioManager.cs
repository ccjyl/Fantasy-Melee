using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")] 
    public PlayerAudioEventSO FXAudioEvent;
    public PlayerAudioEventSO BGMAudioEvent;
    public FloatEventSO VolumeChangeEvent;
    public ViodEventSO PauseEvent;

    [Header("广播")] 
    public FloatEventSO SyncVolumeEvent;
    
    [Header("组件")]
    public AudioSource BGMSource;
    public AudioSource FXSource;
    public AudioMixer audioMixer;
    

    private void OnEnable()
    {
        FXAudioEvent.OnEventRaised += FXEvent;
        BGMAudioEvent.OnEventRaised += BGMEvent;
        VolumeChangeEvent.OnEventRaise += VolumeChange;
        PauseEvent.OnEventRaise += OnPauseEvent;
    }

    private void OnDisable()
    {
        FXAudioEvent.OnEventRaised -= FXEvent;
        BGMAudioEvent.OnEventRaised -= BGMEvent;
        VolumeChangeEvent.OnEventRaise -= VolumeChange;
        PauseEvent.OnEventRaise -= OnPauseEvent;
    }

    private void OnPauseEvent()
    {
        float amount;
        audioMixer.GetFloat("MasterMixer", out amount);
        SyncVolumeEvent?.RaiseEvent(amount);
    }

    private void VolumeChange(float amount)
    {
        audioMixer.SetFloat("MasterMixer", amount);
    }

    private void FXEvent(AudioClip audioClip)
    {
        FXSource.clip = audioClip;
        FXSource.Play();
    }

    private void BGMEvent(AudioClip audioClip)
    {
        BGMSource.clip = audioClip;
        BGMSource.Play();
    }
}
