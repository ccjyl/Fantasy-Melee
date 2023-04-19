using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 普通攻击第二段
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character2State/Atk2", fileName = "Character2_Atk2")]
    public class Character2_Atk2 : Character2StateBase
    {
        public override void Enter()
        {
            base.Enter();
            //状态进入攻击2
            playerController.isAtk2 = true;
            currentSpeed = playerController.playerData.atk2MoveSpeed;
            //设置伤害
            playerController.currentDamage = playerController.playerData.atk2Damage;
        }

        public override void Exit()
        {
            base.Exit();
            isAttack = false;
            playerController.isAtk2 = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //---任意时机---
            if (playerController.inputMode.Skill3)
            {
                //技能3
                stateMachine.SwitchState(typeof(Character2_Skill3));
            }
            else if (playerController.inputMode.Skill2)
            {
                //技能2
                stateMachine.SwitchState(typeof(Character2_Skill2));
            }
            else if (playerController.inputMode.Skill1)
            {
                //技能1
                stateMachine.SwitchState(typeof(Character2_Skill1));
            }

            //---当前动画播放完毕---
            if (IsAnimationFinished)
            {
                if (playerController.inputMode.Move)
                {
                    //跑步
                    stateMachine.SwitchState(typeof(Character2_Run));
                }
                else if (isAttack)
                {
                    //浮空攻击
                    if (playerController.inputMode.Up)
                    {
                        stateMachine.SwitchState(typeof(Character2_LevitateAtk));
                    }
                    else
                    {
                        //普攻1
                        stateMachine.SwitchState(typeof(Character2_Atk1));
                    }
                }
                else if (!playerController.inputMode.Move)
                {
                    //待机
                    stateMachine.SwitchState(typeof(Character2_Idle));
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