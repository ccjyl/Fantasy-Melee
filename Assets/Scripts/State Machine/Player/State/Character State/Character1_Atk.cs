using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 普通攻击
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Atk", fileName = "Character1_Atk")]
    public class Character1_Atk : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = 0f;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (IsAnimationFinished)
            {
                stateMachine.SwitchState(typeof(Character1_Idle));
            }
        }

        public override void PhysicUpdate()
        {
            characterController.SetVelocityX(currentSpeed);
        }
    }
}
