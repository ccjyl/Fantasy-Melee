using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色数据
    /// Author:clof
    /// </summary>
    [CreateAssetMenu(menuName = "Data/PlayerData", fileName = "New CharacterDataSO")]
    public class PlayerData : ScriptableObject
    {
        [Header("移动"),Tooltip("跑步移动速度")] public float runMoveSpeed=5f;
        [Tooltip("跳跃水平移动速度")] public float jumpHorizontalSpeed=5f;
        [Tooltip("跳跃垂直移动速度")] public float jumpVerticalSpeed=5f;
        
        [Header("攻击1"),Tooltip("攻击1移动速度")] public float atk1MoveSpeed=0.2f;
        [Header("攻击2"),Tooltip("攻击2移动速度")] public float atk2MoveSpeed=0.2f;
        [Header("攻击3"),Tooltip("攻击3移动速度")] public float atk3MoveSpeed=0.2f;
        [Header("技能1"),Tooltip("技能1移动速度")] public float skill1MoveSpeed=5f;
        [Tooltip("技能1持续时间")] public float skill1DurationTime = 2f;
        
    }
}
