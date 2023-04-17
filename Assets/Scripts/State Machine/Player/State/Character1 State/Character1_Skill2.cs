using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 技能2
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Skill2", fileName = "Character1_Skill2")]
    public class Character1_Skill2 : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            //消耗蓝量
            playerController.CurrentMp -= playerController.playerData.skill2ExpendMp;
            //设置伤害
            playerController.currentDamage = playerController.playerData.skill2Damage;
            currentSpeed = 0;
        }

        public override void Exit()
        {
            base.Exit();
            isAttack = false;
        }

        public override void LogicUpdate()
        { base.LogicUpdate();
            //---任意时机---
            if (playerController.inputMode.Sprint)
            {
                //冲刺
                stateMachine.SwitchState(typeof(Character1_Sprint));
            }

            //---当前动画播放完毕---
            if (IsAnimationFinished)
            {
                if (playerController.inputMode.Parry)
                {
                    //格挡
                    stateMachine.SwitchState(typeof(Character1_Parry));
                }
                else if (playerController.inputMode.Skill3 )
                {
                    //技能3
                    stateMachine.SwitchState(typeof(Character1_Skill3));
                }
         
                else if (playerController.inputMode.Skill1 )

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
                else if (playerController.inputMode.Move)
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

            //是否下个动画衔接攻击
            if (playerController.inputMode.Attack)
            {
                isAttack = true;
            }
        }

        public override void PhysicUpdate()
        {
            playerController.SetVelocityX(currentSpeed);
        }
    }
}