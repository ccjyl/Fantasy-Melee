using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色2状态机
    /// Author:clof
    /// </summary>
    public class Character2StateMachine : CharacterStateMachine
    {
        private void Start()
        {
            StartState(stateTable[typeof(Character2_Idle)]);//设置默认状态
        }
    }
}
