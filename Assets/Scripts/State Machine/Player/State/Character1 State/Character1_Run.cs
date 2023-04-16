using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 Run
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Run", fileName = "Character1_Run")]
    public class Character1_Run : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = playerController.playerData.runMoveSpeed;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {   //---在地面上---
            if (playerController.IsGrounded)
            {
                if (playerController.inputMode.Parry)
                {
                    //格挡
                    stateMachine.SwitchState(typeof(Character1_Parry));
                }
                else if (playerController.inputMode.Skill1)
                {
                    //技能1
                    stateMachine.SwitchState(typeof(Character1_Skill1));
                }
                else if (!playerController.inputMode.Move)
                {
                    //待机
                    stateMachine.SwitchState(typeof(Character1_Idle));
                }
                else if (playerController.inputMode.Attack)
                {
                    //浮空攻击
                    if (playerController.inputMode.Up)
                    {
                        stateMachine.SwitchState(typeof(Character1_LevitateAtk));
                    }
                    else
                    {
                        //普攻1
                        stateMachine.SwitchState(typeof(Character1_Atk1));
                    }
                }
                else if (playerController.inputMode.Jump)
                {
                    //跳跃
                    stateMachine.SwitchState(typeof(Character1_JumpUp));
                }
            }
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
        }
    }
}