using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
   public GameSceneSO firstLoadScene;
   public GameSceneSO chooseScene;
   [Header("事件监听")]
   public ScenesLoadEventSO sceneLoadEvent;
   //获取玩家的角色选择选择 <>未完成-->完成
   public ChoosePlayerEventSO choPlayerEvent;

   public ViodEventSO GoToChoice;

   [Header("广播")]
   public FadeEventSO fadeEvent;
   //生成完成后发送广播，通知UI刷新角色的Icon <>完成
   public ViodEventSO refreshUIEvent;

   private GameSceneSO currentLoadScene;
   //临时存储场景加载变量
   private GameSceneSO temScene;
   private bool temFade;
   
   //淡入淡出等待时间
   public float waitTime;

   [Header("玩家选择面板")] 
   private GameObject playerCho;
   //临时存储玩家选择
   private GameObject player1;
   private GameObject player2;
   [Header("组件")]
   public CountdownTimer timer;
   
   private void Awake()
   {
      //Addressables.LoadSceneAsync(firstLoadScene.scenesAssetReference, LoadSceneMode.Additive);
      currentLoadScene = firstLoadScene;
      currentLoadScene.scenesAssetReference.LoadSceneAsync(LoadSceneMode.Additive);

   }

   private void OnEnable()
   {
      sceneLoadEvent.sceneLoadEvent += OnLoadRequestEvent;
      choPlayerEvent.OnEventRaise += ChoPlayerEvent;
      GoToChoice.OnEventRaise += GamepverToChoiceScene;
   }

   private void OnDisable()
   {
      sceneLoadEvent.sceneLoadEvent -= OnLoadRequestEvent;
      choPlayerEvent.OnEventRaise -= ChoPlayerEvent;
      GoToChoice.OnEventRaise -= GamepverToChoiceScene;
   }

   void GamepverToChoiceScene()
   {
      GameManager.Instance.gameOver = false;
      temScene = chooseScene;
      OnLoadRequestEvent(temScene,true);
   }

   private void ChoPlayerEvent(GameObject playerPrefab)
   {
      if (playerCho.activeInHierarchy)
      {
         player1 = choPlayerEvent.playerPrefab;
      }
      else
      {
         player2 = choPlayerEvent.playerPrefab;
      }
   }

   private void OnLoadRequestEvent(GameSceneSO sceneToLoad, bool fadeScreen)
   {
      temScene = sceneToLoad;
      temFade = fadeScreen;
      
      //Debug.Log("cc");
      StartCoroutine(UnLoadScene());
   }

   IEnumerator UnLoadScene()
   {
      if (temFade)
      {
         //淡入淡出:变黑
         fadeEvent.FadeIn(waitTime);
      }

      yield return new WaitForSeconds(waitTime);

      if (currentLoadScene!=null)
      {
         currentLoadScene.scenesAssetReference.UnLoadScene();
      }
      LoadNewScene();
   }

   public void LoadNewScene()
   {
      var loadingOption= temScene.scenesAssetReference.LoadSceneAsync(LoadSceneMode.Additive, true);
      loadingOption.Completed += OnLoadCompleted;
      
   }

   private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
   {
      currentLoadScene = temScene;
      if (currentLoadScene.scenesType==ScenesType.Choice)
      {
         //获取选择界面
         playerCho = GameObject.Find("P1");
      }
      
      //如果是比赛场景，生成玩家
      if (currentLoadScene.scenesType==ScenesType.Playing)
      {
         var P1= Instantiate(player1);
         var P2= Instantiate(player2);
         GameManager.Instance.playerP1 = P1;
         GameManager.Instance.playerP2 = P2;
      }
      if (temFade)
      {
         fadeEvent.FadeOut(waitTime);
      }
      if (currentLoadScene.scenesType==ScenesType.Playing)
      {
         //生成完成后发送广播，通知UI刷新角色的Icon
         refreshUIEvent?.RaiseEvent();
         timer.enabled = true;
      }
      else
      {
         timer.enabled = false;
      }
     
   }
}
