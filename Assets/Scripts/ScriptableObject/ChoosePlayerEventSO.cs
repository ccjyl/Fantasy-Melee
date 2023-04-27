using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/ChoosePlayerEventSO")]
public class ChoosePlayerEventSO : ScriptableObject
{
    public GameObject playerPrefab;
    public UnityAction<GameObject> OnEventRaise;

    public void RaiseEvent(GameObject playerPrefabs1)
    {
        playerPrefab = playerPrefabs1;
        OnEventRaise?.Invoke(playerPrefabs1);
       
    }
    
    
}
