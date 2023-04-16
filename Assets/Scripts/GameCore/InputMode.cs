using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 输入方式
    /// Author:CLOF
    /// </summary>
    public class InputMode : MonoBehaviour
    {
        public bool Up => transform.CompareTag("Player1") ? Input.GetKey(KeyCode.W) : Input.GetKey(KeyCode.UpArrow);

        /// <summary>
        /// 格挡
        /// </summary>
        public bool Parry => transform.CompareTag("Player1") ? Input.GetKey(KeyCode.S) : Input.GetKey(KeyCode.DownArrow);

        /// <summary>
        /// 移动
        /// </summary>
        public bool Move => transform.CompareTag("Player1")
            ? Input.GetAxisRaw("Player1Horizontal") != 0
            : Input.GetAxisRaw("Player2Horizontal") != 0;

        public float AxisX => transform.CompareTag("Player1")
            ? Input.GetAxisRaw("Player1Horizontal")
            : Input.GetAxisRaw("Player2Horizontal");

        /// <summary>
        /// 普通攻击
        /// </summary>
        public bool Attack => transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.J) : Input.GetKeyDown(KeyCode.Keypad1);

        /// <summary>
        /// 跳跃
        /// </summary>
        public bool Jump => transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.K) : Input.GetKeyDown(KeyCode.Keypad2);

        /// <summary>
        /// 冲刺
        /// </summary>
        public bool Sprint => transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.L) : Input.GetKeyDown(KeyCode.Keypad3);

        /// <summary>
        /// 远程攻击
        /// </summary>
        public bool RemoteAttack =>
            transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.U) : Input.GetKeyDown(KeyCode.Keypad0);

        /// <summary>
        /// 技能1
        /// </summary>
        public bool Skill1 => transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.H) : Input.GetKeyDown(KeyCode.Keypad4);

        /// <summary>
        /// 技能2
        /// </summary>
        public bool Skill2 => transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.B) : Input.GetKeyDown(KeyCode.Keypad5);

        /// <summary>
        /// 技能3
        /// </summary>
        public bool Skill3 => transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.N) : Input.GetKeyDown(KeyCode.Keypad6);


        /// <summary>
        /// 必杀技
        /// </summary>
        public bool UniqueSkill =>
            transform.CompareTag("Player1") ? Input.GetKeyDown(KeyCode.I) : Input.GetKeyDown(KeyCode.Keypad9);
    }
}