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
    public class Character1_Hit : Character1StateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = 0f;
            timer = 0;
            //受伤中
            playerController.isHitting = true;
            playerController.SetUseGravity(0f); //关闭重力
            playerController.SetVelocityY(0f); //将垂直速度关闭

            //造成伤害
            playerController.CurrentHp -= playerController.attacker.currentDamage;


            //如果攻击者的是 浮空攻击
            if (playerController.attacker.isLevitate)
            {
                //将被攻击者浮空
                playerController.transform.position = playerController.attacker.levitatePosition.position;
            }

            //如果角色朝向等于攻击者的朝向 则改变朝向使其面向攻击者
            if (playerController.transform.localScale.x == playerController.attacker.transform.localScale.x)
            {
                playerController.SetFaceDirection(-playerController.transform.localScale.x);
            }

            //skill1 禁锢
            if (playerController.attacker.isSkill1)
            {
                playerController.isImprison = true;//将被攻击者禁锢
            }

            if (playerController.attacker.isAtk1)
            {
                //受伤计数
                playerController.hitCount++;
                //回复攻击者蓝量
                playerController.attacker.CurrentMp += playerController.attacker.playerData.atk1RecoverMp;
            }

            if (playerController.attacker.isAtk2)
            {
                //受伤计数
                playerController.hitCount++;
                //回复攻击者蓝量
                playerController.attacker.CurrentMp += playerController.attacker.playerData.atk2RecoverMp;
            }
        }

        public override void Exit()
        {
            base.Exit();
            //退出受伤状态
            playerController.isHitting = false;
            playerController.SetUseGravity(1f); //开启重力
            playerController.ResetHit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
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
            playerController.SetVelocityX(currentSpeed);
        }
    }
}