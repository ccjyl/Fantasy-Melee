using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// Victory 状态
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Victory", fileName = "Character1_Victory")]
    public class Character1_Victory : CharacterStateBase
    {
        public override void Enter()
        {
            base.Enter();
            currentSpeed = 0f;
            Debug.Log("胜利");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicUpdate()
        {
            playerController.SetVelocityX(currentSpeed);
        }
    }
}