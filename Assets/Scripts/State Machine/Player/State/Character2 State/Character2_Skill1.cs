using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 技能1
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character2State/Skill1", fileName = "Character2_Skill1")]
    public class Character2_Skill1 : Character2StateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = 0;
            //消耗蓝量
            playerController.CurrentMp -= playerController.playerData.skill1ExpendMp;
            //设置伤害
            playerController.currentDamage = playerController.playerData.skill1Damage;

            playerController.isSkill1 = true;
        }

        public override void Exit()
        {
            base.Exit();
            isAttack = false;
            playerController.isSkill1 = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            //---动画播放完毕---
            if (IsAnimationFinished)
            {
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

                else if (isAttack)
                {
                    //浮空攻击
                    if (playerController.inputMode.Up)
                    {
                        stateMachine.SwitchState(typeof(Character2_LevitateAtk));
                    }
                    else
                    {
                        //普攻2
                        stateMachine.SwitchState(typeof(Character2_Atk1));
                    }
                }
                else if (playerController.inputMode.Move)
                {
                    //跑步
                    stateMachine.SwitchState(typeof(Character2_Run));
                }
                else if (!playerController.inputMode.Move)
                {
                    //待机
                    stateMachine.SwitchState(typeof(Character2_Idle));
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