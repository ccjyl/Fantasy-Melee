using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PlayAudioEventSO")]
public class PlayerAudioEventSO :ScriptableObject
{
   public UnityAction<AudioClip> OnEventRaised;

   public void RaisedEvent(AudioClip audioClip)
   {
      OnEventRaised?.Invoke(audioClip);
   }
}
