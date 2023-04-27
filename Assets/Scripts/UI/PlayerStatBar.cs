using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image hpImage;
    public Image mpImage;

    /// <summary>
    /// 刷新血量
    /// </summary>
    /// <param name="persentage"></param>
    public void OnHealthChange(float hpPersentage)
    {
        hpImage.fillAmount = hpPersentage;
    }

    /// <summary>
    /// 刷新蓝量
    /// </summary>
    /// <param name="mpPersentage"></param>
    public void OnMpChange(float mpPersentage)
    {
        mpImage.fillAmount = mpPersentage;
    }
}
