using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色状态机
    /// Author:clof
    /// </summary>
    public class CharacterStateMachine : StateMachine
    {
        [Header("具体角色状态SO"), SerializeField] private CharacterStateBase[] _states;

        //引用
        private Animator _animator;
        private PlayerController _playerController;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            stateTable = new Dictionary<Type, IState>(_states.Length); //初始化字典
            foreach (var state in _states)
            {
                state.Initialize(_animator, this, _playerController); //将引用传入至状态中
                stateTable.Add(state.GetType(), state); //将激活的状态放入字典保存
            }
        }
    }
}