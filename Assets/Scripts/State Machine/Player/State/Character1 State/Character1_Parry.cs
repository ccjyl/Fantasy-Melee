using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 格挡
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Parry", fileName = "Character1_Parry")]
    public class Character1_Parry : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = 0;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            //---取消格挡时---
            if (!playerController.inputMode.Parry)
            {
                if (playerController.inputMode.Skill1)
                {
                    //技能1
                    stateMachine.SwitchState(typeof(Character1_Skill1));
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
                else if (playerController.inputMode.Jump)
                {
                    //跳跃
                    stateMachine.SwitchState(typeof(Character1_JumpUp));
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
            playerController.SetVelocityX(currentSpeed); //将刚体x轴设置为0
        }
    }
}