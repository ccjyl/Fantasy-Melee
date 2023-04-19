using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 跳起
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character2State/JumpUp", fileName = "Character2_JumpUp")]
    public class Character2_JumpUp : Character2StateBase
    {
        public override void Enter()
        {
            base.Enter();
          
            currentSpeed = 0f;
            if (playerController.isDoubleJump)
            {
                playerController.SetVelocityY(playerController.playerData.jumpVerticalSpeed*2);
                playerController.isDoubleJump = false;
            }
            else
            {
                playerController.SetVelocityY(playerController.playerData.jumpVerticalSpeed);
            }
           
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //---任意时机---
     
            
            //---在地面上---
            if (playerController.IsGrounded)
            {
                if (playerController.inputMode.Jump)
                {
                    //跳跃
                    stateMachine.SwitchState(typeof(Character2_JumpUp));
                }
                else if (playerController.inputMode.Move)
                {
                    //移动
                    stateMachine.SwitchState(typeof(Character2_Run));
                }
                else if (!playerController.inputMode.Move)
                {
                    //待机
                    stateMachine.SwitchState(typeof(Character2_Idle));
                }
            }else if (playerController.inputMode.Move)
            {
                //空中移动
                stateMachine.SwitchState(typeof(Character2_JumpAir));
            }
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
        }
    }
}