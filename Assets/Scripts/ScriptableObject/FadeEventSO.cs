using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO :ScriptableObject
{
   public UnityAction<Color, float, bool> OnEventRaised;
   
   /// <summary>
   /// 逐渐变黑
   /// </summary>
   /// <param name="time"></param>
   public void FadeIn(float time)
   {
      RaiseEvent(Color.black,time,true);
   }

   /// <summary>
   /// 逐渐变透明
   /// </summary>
   /// <param name="time"></param>
   public void FadeOut(float time)
   {
      RaiseEvent(Color.clear, time,false);
   }

   public void RaiseEvent(Color color,float time,bool fadeIn)
   {
      OnEventRaised?.Invoke(color,time,fadeIn);
   }
}
