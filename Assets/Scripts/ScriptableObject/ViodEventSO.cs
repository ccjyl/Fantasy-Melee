using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/ViodEventSO")]
public class ViodEventSO :ScriptableObject
{
    public UnityAction OnEventRaise;

    public void RaiseEvent()
    {
        OnEventRaise?.Invoke();
    }
}
