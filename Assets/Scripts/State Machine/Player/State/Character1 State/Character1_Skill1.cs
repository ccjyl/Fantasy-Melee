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
        private float _timer;

        public override void Enter()
        {
            base.Enter();
            currentSpeed = playerController.playerData.skill1MoveSpeed;
            _timer = 0f; //计时器
        }

        public override void Exit()
        {
            base.Exit();
            isAttack = false;
        }

        public override void LogicUpdate()
        {
            //---当前动画播放完毕---
            if (_timer > playerController.playerData.skill1DurationTime)
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
                        stateMachine.SwitchState(typeof(Character1_Atk1));
                    }
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
            _timer += Time.deltaTime;
        }

        public override void PhysicUpdate()
        {
            playerController.Move(currentSpeed);
        }
    }
}