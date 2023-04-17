using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 状态机基类
/// 1、持有所有的状态类，并对其进行管理和切换
/// 2、负责进行当前状态的更新 
/// Author:CLOF
/// </summary>
public class StateMachine : MonoBehaviour
{
    private IState _currentState;//当前使用状态
    protected Dictionary<System.Type, IState> stateTable;//状态表，用来记录当前物体的所有状态

    #region 状态机更新

    private void Update()
    {
        _currentState.LogicUpdate();//逻辑更新
    }

    private void FixedUpdate()
    {
        _currentState.PhysicUpdate();//物理更新
    }

    #endregion

    #region 状态切换

    /// <summary>
    /// 设置新状态，并启动
    /// 通过它设置默认状态
    /// </summary>
    /// <param name="newState"></param>
    protected void StartState(IState newState)
    {
        _currentState = newState;//设置新状态
        _currentState.Enter();//启动当前状态
    }
    
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newStateType">新的状态名</param>
    public void SwitchState(Type newStateType)
    {
        _currentState.Exit();//退出上一个状态
        StartState(stateTable[newStateType]);//设置新状态，并启动
    }
    
    
    
    #endregion

    #region 状态比较
    /// <summary>
    /// 当前状态和传入类型是否一致
    /// </summary>
    /// <param name="stateType">类型</param>
    public bool CompareState(Type stateType)
    {
        return _currentState.GetType() == stateType;
    }

    #endregion
    
}