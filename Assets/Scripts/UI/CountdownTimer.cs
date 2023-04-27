using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//计时器
public class CountdownTimer : MonoBehaviour
{
    private int remainingTime = 120;
    public Image Timer;

    private void Start()
    {
        InvokeRepeating("ReduceTime",1f,1f);
    }

    private void Update()
    {
        Timer.fillAmount = remainingTime / 120.0f;
    }

    void ReduceTime()
    {
        if (remainingTime>0)
        {
            remainingTime--;
        }
        else
        {
            CancelInvoke("ReduceTime");
        }
    }
    

}
