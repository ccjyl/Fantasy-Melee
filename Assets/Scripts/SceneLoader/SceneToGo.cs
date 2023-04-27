using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneToGo : MonoBehaviour
{
   public ScenesLoadEventSO scenesLoadEvent;

   public GameSceneSO scene;

   public void ToScene()
   {
      Debug.Log("开始游戏");
      
      scenesLoadEvent.RaiseSceneLoadEvent(scene,true);
   }
}
