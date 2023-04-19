using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色2状态基类
    /// Author:clof
    /// </summary>
    public class Character2StateBase : CharacterStateBase
    {
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //受到攻击
            if (playerController.underAttack)
            {
                //判断结束
                playerController.underAttack = false;
                //不是格挡状态 则受伤
                if (playerController.isCanHit)
                {
                    stateMachine.SwitchState(typeof(Character2_Hit));
                }
            }
        }
    }
}
