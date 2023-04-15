using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// Idle 状态
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Idle", fileName = "Character1_Idle")]
    public class Character1_Idle : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = 0f;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (characterController.inputMode.Left || characterController.inputMode.Right)
            {
                //跑步
                stateMachine.SwitchState(typeof(Character1_Run));
            }
            else if (characterController.inputMode.Attack)
            {
                //普攻
                stateMachine.SwitchState(typeof(Character1_Atk));
            }
            else if (characterController.inputMode.Up)
            {
                //跳跃
                stateMachine.SwitchState(typeof(Character1_Jump));
            }
        }

        public override void PhysicUpdate()
        {
            characterController.SetVelocityX(currentSpeed); //将刚体x轴设置为0
        }
    }
}