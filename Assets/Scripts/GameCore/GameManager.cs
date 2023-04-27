using System;
using System.Collections;
using System.Collections.Generic;
using FantasyMelee;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector]public bool gameOver = false;
    [HideInInspector] public bool beginKO;
    [Header("组件")]
    public Image timer;
    [HideInInspector]public GameObject playerP1;
    [HideInInspector]public GameObject playerP2;

    private void Awake()
    {
        if (Instance==null)
            Instance = this;
        else
            Destroy(this.gameObject);
        
    }

    private void Update()
    {
        //时间结束停止游戏
        if (timer.fillAmount<=0.1f)
        {
            beginKO = true;
        }
        CheckPlayerHP();
    }
    
    //检测玩家生命
    public void CheckPlayerHP()
    {
        if (playerP1!=null)
        {
            var playerHP1= playerP1.GetComponent<PlayerController>();
            var playerHP2= playerP2.GetComponent<PlayerController>();
            if (playerHP1._currentHp <= 0 || playerHP2._currentHp <= 0)
            {
                beginKO = true;
            }
            
        }
    }
}
