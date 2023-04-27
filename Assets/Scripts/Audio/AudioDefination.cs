using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    [Header("广播")]
    public PlayerAudioEventSO playAudioEvent;
    public AudioClip audioClip;
    public bool playOnEnable;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        playAudioEvent.RaisedEvent(audioClip);
    }
}
