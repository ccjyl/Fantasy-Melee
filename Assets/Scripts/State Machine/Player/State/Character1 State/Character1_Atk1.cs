using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 普通攻击第一段
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Atk1", fileName = "Character1_Atk1")]
    public class Character1_Atk1 : Character1StateBase
    {
        public override void Enter()
        {
            base.Enter();
            //状态进入攻击2
            playerController.isAtk1 = true;

            currentSpeed = playerController.playerData.atk1MoveSpeed;
            //设置伤害
            playerController.currentDamage = playerController.playerData.atk1Damage1;
        }

        public override void Exit()
        {
            base.Exit();
            isAttack = false;
            playerController.isAtk1 = false;
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

            //---当前动画播放完毕---
            if (IsAnimationFinished)
            {
                if (playerController.inputMode.Parry)
                {
                    //格挡
                    stateMachine.SwitchState(typeof(Character1_Parry));
                }

                else if (playerController.inputMode.Move)
                {
                    //跑步
                    stateMachine.SwitchState(typeof(Character1_Run));
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
                        stateMachine.SwitchState(typeof(Character1_Atk2));
                    }
                }
                else if (!playerController.inputMode.Move)
                {
                    //待机
                    stateMachine.SwitchState(typeof(Character1_Idle));
                }
            }

            //是否要连击
            if (playerController.inputMode.Attack)
            {
                isAttack = true;
            }
        }

        public override void PhysicUpdate()
        {
            playerController.SetVelocityX(currentSpeed * playerController.transform.localScale.x);
        }
    }
}