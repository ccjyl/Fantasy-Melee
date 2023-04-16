using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 跳跃
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/JumpUp", fileName = "Character1_JumpUp")]
    public class Character1_JumpUp : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
          
            currentSpeed = 0f;
            playerController.SetVelocityY(playerController.playerData.jumpVerticalSpeed);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            //---任意时机---
            if (playerController.inputMode.Skill1)
            {
                //技能1
                stateMachine.SwitchState(typeof(Character1_Skill1));
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
            }else if (playerController.inputMode.Move)
            {
                //空中移动
                stateMachine.SwitchState(typeof(Character1_JumpAir));
            }
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
        }
    }
}