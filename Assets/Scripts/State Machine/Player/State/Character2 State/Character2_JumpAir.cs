using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 空中移动
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character2State/JumpAir", fileName = "Character2_JumpAir")]
    public class Character2_JumpAir : Character2StateBase
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
            }
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
        }
    }
}