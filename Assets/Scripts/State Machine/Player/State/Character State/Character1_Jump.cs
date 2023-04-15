using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 跳跃
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Jump", fileName = "Character1_Jump")]
    public class Character1_Jump : CharacterStateBase
    {
        [Header("移动"), SerializeField, Tooltip("垂直速度")] private float verticalSpeed = 5f;
        [SerializeField, Tooltip("横向速度")] private float horizontalSpeed = 5f;

        public override void Enter()
        {
            base.Enter();
            currentSpeed = horizontalSpeed;
            characterController.SetVelocityY(verticalSpeed);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (characterController.IsGrounded)
            {
                //跳跃
                if (characterController.inputMode.Up)
                {
                    stateMachine.SwitchState(typeof(Character1_Jump));
                }
                else if (characterController.inputMode.Left || characterController.inputMode.Right)
                {
                    stateMachine.SwitchState(typeof(Character1_Run));
                }
                else if (!characterController.inputMode.Left && !characterController.inputMode.Right)
                {
                    stateMachine.SwitchState(typeof(Character1_Idle));
                }
            }
        }

        public override void PhysicUpdate()
        {
            characterController.Move(currentSpeed * characterController.transform.localScale.x);
        }
    }
}