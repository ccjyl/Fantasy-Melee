using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 空中移动
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/JumpAir", fileName = "Character1_JumpAir")]
    public class Character1_JumpAir : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = playerController.playerData.jumpHorizontalSpeed;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //---任意时机---
            if (playerController.inputMode.Sprint)
            {
                //冲刺
                stateMachine.SwitchState(typeof(Character1_Sprint));
            }

            //---在地面上---
            if (playerController.IsGrounded)
            {
                if (playerController.inputMode.Jump)
                {
                    //跳跃
                    stateMachine.SwitchState(typeof(Character1_JumpUp));
                }
                else if (playerController.inputMode.Move)
                {
                    //移动
                    stateMachine.SwitchState(typeof(Character1_Run));
                }
                else if (!playerController.inputMode.Move)
                {
                    //待机
                    stateMachine.SwitchState(typeof(Character1_Idle));
                }
            }
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
        }
    }
}