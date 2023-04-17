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
            base.LogicUpdate();
            //---在地面上---
            if (playerController.IsGrounded)
            {
                if (playerController.inputMode.Parry)
                {
                    //格挡
                    stateMachine.SwitchState(typeof(Character1_Parry));
                }
                else if (playerController.inputMode.Sprint)
                {
                    //冲刺
                    stateMachine.SwitchState(typeof(Character1_Sprint));
                }
                else if (playerController.inputMode.Skill3)
                {
                    //技能3
                    stateMachine.SwitchState(typeof(Character1_Skill3));
                }
                else if (playerController.inputMode.Skill2)
                {
                    //技能2
                    stateMachine.SwitchState(typeof(Character1_Skill2));
                }
                else if (playerController.inputMode.Skill1)
                {
                    //技能1
                    stateMachine.SwitchState(typeof(Character1_Skill1));
                }
                else if (playerController.inputMode.Jump)
                {
                    //跳跃
                    stateMachine.SwitchState(typeof(Character1_JumpUp));
                }
                else if (playerController.inputMode.Move)
                {
                    //跑步
                    stateMachine.SwitchState(typeof(Character1_Run));
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
            }
        }

        public override void PhysicUpdate()
        {
            playerController.SetVelocityX(currentSpeed); //将刚体x轴设置为0
        }
    }
}