using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1 Run
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "State/Character1State/Run", fileName = "Character1_Run")]
    public class Character1_Run : CharacterStateBase
    {
        [Header("移动"), SerializeField, Tooltip("速度")] private float runSpeed = 5f;

        public override void Enter()
        {
            base.Enter();
            currentSpeed = runSpeed;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (!characterController.inputMode.Left && !characterController.inputMode.Right)
            {
                stateMachine.SwitchState(typeof(Character1_Idle));
            }
            else if (characterController.inputMode.Attack)
            {
                stateMachine.SwitchState(typeof(Character1_Atk));
            }
            else if (characterController.inputMode.Up)
            {
                stateMachine.SwitchState(typeof(Character1_Jump));
            }
        }

        public override void PhysicUpdate()
        {
            characterController.Move(currentSpeed * characterController.transform.localScale.x);
        }
    }
}