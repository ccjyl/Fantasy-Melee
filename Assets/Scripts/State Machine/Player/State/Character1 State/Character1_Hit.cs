using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// Hit 状态
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Hit", fileName = "Character1_Hit")]
    public class Character1_Hit : CharacterStateBase
    {
        public override void Enter() 
        {
            base.Enter();
            currentSpeed = 0f;
            timer = 0;
            isHit = true;
            currentSpeed = playerController.playerData.hitMoveSpeed;
            playerController.SetUseGravity(0f); //关闭重力
        }

        public override void Exit()
        {
            base.Exit();
            playerController.SetUseGravity(1f); //开启重力
        }

        public override void LogicUpdate()
        {
            //---持续时间结束---
            if (timer > playerController.playerData.hitDurationTime)
            {
                playerController.SetUseGravity(1f); //开启重力

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
                else if (playerController.IsGrounded)
                {
                    //---在地面---
                    if (playerController.inputMode.Move)
                    {
                        //跑步
                        stateMachine.SwitchState(typeof(Character1_Run));
                    }
                    else
                    {
                        //待机
                        stateMachine.SwitchState(typeof(Character1_Idle));
                    }
                }
            }

            //计时
            timer += Time.deltaTime;
        }

        public override void PhysicUpdate()
        {
            playerController.SetVelocityX(currentSpeed * playerController.transform.localScale.x);
        }
    }
}