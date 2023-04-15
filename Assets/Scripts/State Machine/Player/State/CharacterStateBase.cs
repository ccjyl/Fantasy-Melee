using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色状态基类
    /// Author:clof
    /// </summary>
    public class CharacterStateBase : StateBase
    {
        protected CharacterController characterController;
        protected float currentSpeed;
        /// <summary>
        /// 引用初始化
        /// </summary>
        public void Initialize(Animator animator, StateMachine stateMachine, CharacterController characterController)
        {
            this.animator = animator;
            this.stateMachine = stateMachine;
            this.characterController = characterController;
        }
        
        
        
    }
}
