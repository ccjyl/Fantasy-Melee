using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色1状态机
    /// Author:clof
    /// </summary>
    public class Character1StateMachine : CharacterStateMachine
    {
        private void Start()
        {
            StartState(stateTable[typeof(Character1_Idle)]);//设置默认状态
        }
    }
}
