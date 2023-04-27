using System;
using System.Collections;
using System.Collections.Generic;
using FantasyMelee;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
   public PlayerStatBar playerStatBar;
   public PlayerStatBar playerStatBar2;

   [Header("事件监听")]
   public PlayerEventSO healthEvent;
   public PlayerEventSO healthEvent2;
   //监听生成玩家后的选择刷新UI玩家的Icon<>未完成
   public ViodEventSO RefreshUIEvent;
   public FloatEventSO SyncVolumeEvent;

   [Header("广播")] 
   public ViodEventSO PauseEvent;

   [Header("组件")] 
   public Button buttonSet;
   public GameObject setPanel;
   public GameObject gameOverPanel;
   public Image gameOverImage;
   public Slider mainVolume;
   
   //临时存储玩家选择
   private GameObject player1;
   private GameObject player2;
   //玩家Icon[偷懒写法，后续优化]
   [Header("玩家Icon[偷懒写法，后续优化]")]
   public Image P1Icon;
   public Image P2Icon;
   public Sprite guanPingP1;
   public Sprite guanPingP2;
   public Sprite zhenJiP1;
   public Sprite zhenJiP2;
   public Sprite taiShiCiP1;
   public Sprite taiShiCiP2;

   private void Awake()
   {
      buttonSet.onClick.AddListener(ShoePanel);
      gameOverPanel.SetActive(false);
   }

   private void Update()
   {
      //游戏结束启动游戏结束面板
      if (GameManager.Instance.beginKO)
      {
         gameOverPanel.SetActive(true);
         GameManager.Instance.beginKO = false;
         gameOverImage.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1f), 2f);
         Destroy(player1);
         Destroy(player2);
      }
   }

   private void OnEnable()
   {
      healthEvent.OnEventRaised += OnHealthEvent;
      healthEvent.OnEventRaised += OnMpEvent;
      healthEvent2.OnEventRaised += OnHealthEvent2;
      healthEvent2.OnEventRaised += OnMpEvent2;
      RefreshUIEvent.OnEventRaise += OnRefreshUIEvent;
      SyncVolumeEvent.OnEventRaise += OnSyncVolumeEvent;
   }

   private void OnDisable()
   {
      healthEvent.OnEventRaised -= OnHealthEvent;
      healthEvent.OnEventRaised -= OnMpEvent;
      healthEvent2.OnEventRaised -= OnHealthEvent2;
      healthEvent2.OnEventRaised -= OnMpEvent2;
      RefreshUIEvent.OnEventRaise -= OnRefreshUIEvent;
      SyncVolumeEvent.OnEventRaise -= OnSyncVolumeEvent;
   }

   private void OnSyncVolumeEvent(float amount)
   {
      mainVolume.value = amount;
   }

   private void OnRefreshUIEvent()
   {
      //后续优化
      player1 = GameObject.FindWithTag("Player1");
      player2 = GameObject.FindWithTag("Player2");
      switch (player1.name)
      {
         case "GuanPingA(Clone)":
            P1Icon.sprite = guanPingP1;
            break;
         case "ZhenJiA(Clone)" :
            P1Icon.sprite = zhenJiP1;
            break;
         case "TaiShiCiA(Clone)" :
            P1Icon.sprite = taiShiCiP1;
            break;
      }
      switch (player2.name)
      {
         case "GuanPingB(Clone)":
            P2Icon.sprite = guanPingP2;
            break;
         case "ZhenJiB(Clone)" :
            P2Icon.sprite = zhenJiP2;
            break;
         case "TaiShiCiB(Clone)" :
            P2Icon.sprite = taiShiCiP2;
            break;
      }
   }

   void ShoePanel()
   {
      if (setPanel.activeInHierarchy)
      {
         setPanel.SetActive(false);
         Time.timeScale = 1;
      }
      else
      {
         PauseEvent?.RaiseEvent();
         setPanel.SetActive(true);
         Time.timeScale = 0;
      }
   }

   private void OnMpEvent(PlayerController controller)
   {
      var persentage = controller._currentMp / controller.playerData.maxMp;
      playerStatBar.OnMpChange(persentage);
   }

   private void OnMpEvent2(PlayerController controller)
   {
      var persentage = controller._currentMp / controller.playerData.maxMp;
      playerStatBar2.OnMpChange(persentage);
   }

   private void OnHealthEvent2(PlayerController controller)
   {
      var persentage = controller._currentHp / controller.playerData.maxHp;
      playerStatBar2.OnHealthChange(persentage);
   }

   private void OnHealthEvent(PlayerController controller)
   {
      var persentage = controller._currentHp / controller.playerData.maxHp;
      playerStatBar.OnHealthChange(persentage);
   }
   
   //退出游戏
   public void QuitGame()
   {
      Application.Quit();
   }
   //继续游戏
   public void ContinueGame()
   {
      Time.timeScale=1;
   }
}
