using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;


/// <summary>
/// 状态基类
/// Author:CLOF
/// </summary>
public class StateBase : ScriptableObject, IState
{
    [Header("动画设置"), SerializeField, Tooltip("动画片段名")] private string animationName;
    [SerializeField, Tooltip("是否使用过渡")] private bool isUseDuration = false;
    [SerializeField, Tooltip("是否使用固定时间过渡")] private bool isFixedDuration = true;
    [SerializeField, Tooltip("过渡持续时间(s)")] private float transitionDuration = 0.25f;
    [SerializeField, Tooltip("动画偏移时间(s)")] private float animationStartOffset = 0f;
    [SerializeField, Tooltip("动画所在层级")] private int animationLayer = 0;
    [SerializeField, Range(0f, 1f), Tooltip("过渡持续时间百分比值(%)")] private float transitionDurationPercent = 0.1f;
    [SerializeField, Range(0f, 1f), Tooltip("动画偏移时间百分比值(%)")] private float animationStartOffsetPercent = 0f;
    [SerializeField, Range(0f, 1f), Tooltip("动画过渡当前动画需要执行的进度百分比值(%)")] private float normalizedTransitionTime = 0f;

    //引用
    protected Animator animator;
    protected StateMachine stateMachine; //状态机

    //私有
    private int _animationHashId; //动画hash值
    private float _animationStartTime; //当前动画开始时间

    #region 当前动画时间

    /// <summary>
    /// 当前状态持续时间
    /// </summary>
    protected float AnimationDuration => Time.time - _animationStartTime;

    /// <summary>
    /// 当前动画播放完毕
    /// </summary>
    protected bool IsAnimationFinished =>
        AnimationDuration >= animator.GetCurrentAnimatorStateInfo(animationLayer).length;

    #endregion

    private void OnEnable()
    {
        _animationHashId = Animator.StringToHash(animationName);
    }

    #region 状态行为

    public virtual void Enter()
    {
        if (animationName != null)
        {
            //是否使用过渡
            if (isUseDuration)
            {
                //是否使用固定时间过渡
                if (isFixedDuration)
                {
                    animator.CrossFadeInFixedTime(_animationHashId, transitionDuration, animationLayer,
                        animationStartOffset, normalizedTransitionTime);
                }
                else
                {
                    //使用百分比时间过渡
                    animator.CrossFadeInFixedTime(_animationHashId, transitionDurationPercent, animationLayer,
                        animationStartOffsetPercent, normalizedTransitionTime);
                }
            }
            else
            {
                //不使用过渡
                animator.Play(_animationHashId, animationLayer);
            }
        }

        _animationStartTime = Time.time; //记录动画开始时间
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicUpdate() { }

    #endregion
}