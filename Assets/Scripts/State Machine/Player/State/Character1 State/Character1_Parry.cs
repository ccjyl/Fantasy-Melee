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
            timer = 0;
        }

        public override void Exit()
        {
            base.Exit();
            playerController.isCanParry = false;
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

            //---取消格挡时---
            if (!playerController.inputMode.Parry || timer > playerController.playerData.parryDurationTime)
            {
                if (playerController.inputMode.Skill3)
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

            timer += Time.deltaTime;
        }

        public override void PhysicUpdate()
        {
            playerController.SetVelocityX(currentSpeed); //将刚体x轴设置为0
        }
    }
}