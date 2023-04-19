using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    ///  技能3
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character2State/Skill3", fileName = "Character2_Skill3")]
    public class Character2_Skill3 : Character1StateBase
    {
        public override void Enter()
        {
            base.Enter();
            //消耗蓝量
            playerController.CurrentMp -= playerController.playerData.skill3ExpendMp;
            //伤害值
            playerController.currentDamage = playerController.playerData.skill3Damage1;
            currentSpeed = 0;
        }

        public override void Exit()
        {
            base.Exit();
            isAttack = false;
        }

        public override void LogicUpdate()
        { base.LogicUpdate();
            
            
            //---当前动画播放完毕---
            if (IsAnimationFinished)
            {
                if (playerController.inputMode.Skill2 )

                {
                    //技能2
                    stateMachine.SwitchState(typeof(Character2_Skill2));
                }
                else if (playerController.inputMode.Skill1 )

                {
                    //技能1
                    stateMachine.SwitchState(typeof(Character2_Skill1));
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