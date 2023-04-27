using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/ScenesLoadEventSO")]
public class ScenesLoadEventSO :ScriptableObject
{
    public UnityAction<GameSceneSO, bool> sceneLoadEvent;

    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="scene">目标场景</param>
    /// <param name="fadeScreen">是否需要淡入淡出</param>
    public void RaiseSceneLoadEvent(GameSceneSO scene, bool fadeScreen)
    {
        sceneLoadEvent?.Invoke(scene,fadeScreen);
    }
}
