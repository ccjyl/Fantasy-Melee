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
        protected PlayerController playerController;
        protected float currentSpeed;
        protected bool isAttack;//再次攻击
        protected float timer;//计时器
        /// <summary>
        /// 引用初始化
        /// </summary>
        public void Initialize(Animator animator, StateMachine stateMachine, PlayerController playerController)
        {
            this.animator = animator;
            this.stateMachine = stateMachine;
            this.playerController = playerController;
        }
    }
}
