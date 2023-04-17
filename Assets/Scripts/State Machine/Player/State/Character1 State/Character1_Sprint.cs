using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 冲刺
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Sprint", fileName = "Character1_Sprint")]
    public class Character1_Sprint : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = playerController.playerData.sprintMoveSpeed;
            timer = 0f; //计时器
            playerController.SetUseGravity(0); //取消重力
            //消耗mp
            playerController.CurrentMp -= playerController.playerData.sprintExpendMp;
        }

        public override void Exit()
        {
            base.Exit();
            isAttack = false;
            playerController.SetUseGravity(1); //取消重力
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
                else if (isAttack)
                {
                    //浮空攻击
                    if (playerController.inputMode.Up)
                    {
                        stateMachine.SwitchState(typeof(Character1_LevitateAtk));
                    }
                    else
                    {
                        //普攻2
                        stateMachine.SwitchState(typeof(Character1_Atk1));
                    }
                }

                //是否下个动画衔接攻击
                if (playerController.inputMode.Attack)
                {
                    isAttack = true;
                }
            }

            //---持续时间结束---
            if (timer > playerController.playerData.sprintDurationTime)
            {
                if (playerController.IsGrounded)
                {
                    if (playerController.inputMode.Move)
                    {
                        //跑步
                        stateMachine.SwitchState(typeof(Character1_Run));
                    }
                    else if (!playerController.inputMode.Move)
                    {
                        //待机
                        stateMachine.SwitchState(typeof(Character1_Idle));
                    }
                }
                else
                {
                    //控制移动
                    stateMachine.SwitchState(typeof(Character1_JumpAir));
                }
            }

            //计时
            timer += Time.deltaTime;
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
            if (!playerController.inputMode.Move)
            {
                playerController.SetVelocityX(currentSpeed * playerController.transform.localScale.x);
            }
         
        }
    }
}