using System.Collections;
using System.Collections.Generic;
using FantasyMelee;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/PlayerEventSO")]
public class PlayerEventSO : ScriptableObject
{
    public UnityAction<PlayerController> OnEventRaised;

    public void RaisedEvent(PlayerController playerController)
    {
        OnEventRaised?.Invoke(playerController);
    }
}
