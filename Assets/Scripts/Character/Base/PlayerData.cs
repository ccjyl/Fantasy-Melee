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
        [Header("移动"), Tooltip("跑步移动速度")] public float runMoveSpeed = 5f;
        [Tooltip("跳跃水平移动速度")] public float jumpHorizontalSpeed = 5f;
        [Tooltip("跳跃垂直移动速度")] public float jumpVerticalSpeed = 5f;

        [Header("Hp"), Tooltip("最大hp")] public float maxHp = 300f;
        [Header("Mp"), Tooltip("最大mp")] public float maxMp = 100f;
        [Tooltip("回复时间间隔")] public float recoverMpTime = 1f;
        [Tooltip("每次回复蓝量")] public float recoverMp = 1f;


        [Header("Atk1"), Tooltip("偏移速度")] public float atk1MoveSpeed = 0.2f;
        [Tooltip("伤害值")] public float atk1Damage1 = 2f;
        [Tooltip("伤害值")] public float atk1Damage2 = 2f;
        [Tooltip("击中恢复Mp")] public float atk1RecoverMp = 2f;

        [Header("Atk2"), Tooltip("偏移速度")] public float atk2MoveSpeed = 0.2f;
        [Tooltip("伤害值")] public float atk2Damage = 3f;
        [Tooltip("击中恢复Mp")] public float atk2RecoverMp = 2f;

        [Header("Levitate Atk"), Tooltip("偏移速度")] public float levitateAtkMoveSpeed = 0.2f;
        [Tooltip("伤害值")] public float levitateAtkDamage = 5f;

        [Header("Skill1"), Tooltip("偏移速度")] public float skill1MoveSpeed = 5f;
        [Tooltip("持续时间")] public float skill1DurationTime = 1f;
        [Tooltip("伤害值")] public float skill1Damage = 2f;
        [Tooltip("蓝量消耗")] public float skill1ExpendMp = 5f;
        [Tooltip("禁锢时间")] public float skill1ImprisonTime = 5f;

        [Header("Skill2"), Tooltip("偏移速度")] public float skill2MoveSpeed = 0f;
        [Tooltip("伤害值")] public float skill2Damage = 5f;
        [Tooltip("蓝量消耗")] public float skill2ExpendMp = 15f;

        [Header("Skill3"), Tooltip("偏移速度")] public float skill3MoveSpeed = 0f;
        [Tooltip("一段伤害")] public float skill3Damage1 = 10f;
        [Tooltip("二段伤害值")] public float skill3Damage2 = 5f;
        [Tooltip("蓝量消耗")] public float skill3ExpendMp = 20f;

        [Header("冲刺"), Tooltip("冲刺移动速度")] public float sprintMoveSpeed = 20f;
        [Tooltip("冲刺持续时间")] public float sprintDurationTime = 0.3f;
        [Tooltip("冲刺消耗")] public float sprintExpendMp = 5f;

        [Header("格挡"), Tooltip("格挡持续时间")] public float parryDurationTime = 3f;
        [Tooltip("格挡冷却时间")] public float parryCoolTime = 0.4f;

        [Header("受击"), Tooltip("偏移速度")] public float hitMoveSpeed = 0.3f;
        [Tooltip("受击持续")] public float hitDurationTime = 0.5f;
        [Tooltip("无敌时间")] public float invincibleDurationTime = 1f;
        [Tooltip("重置计数时间")] public float resetHitTime = 2f;
    }
}