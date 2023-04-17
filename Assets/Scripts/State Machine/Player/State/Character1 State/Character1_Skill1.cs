using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 技能1
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Skill1", fileName = "Character1_Skill1")]
    public class Character1_Skill1 : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = playerController.playerData.skill1MoveSpeed;
            //消耗蓝量
            playerController.CurrentMp -= playerController.playerData.skill1ExpendMp;
            //设置伤害
            playerController.currentDamage = playerController.playerData.skill1Damage;
            
            timer = 0f; //计时器
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
            //---持续时间结束---
            if (timer > playerController.playerData.skill1DurationTime)
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
                else if (playerController.inputMode.Skill2 )

                {
                    //技能2
                    stateMachine.SwitchState(typeof(Character1_Skill2));
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
            //计时
            timer += Time.deltaTime;
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
        }
    }
}