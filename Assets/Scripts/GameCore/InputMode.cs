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
        public bool Down => transform.CompareTag("Player1") ? Input.GetKey(KeyCode.S) : Input.GetKey(KeyCode.DownArrow);
        public bool Left => transform.CompareTag("Player1") ? Input.GetKey(KeyCode.A) : Input.GetKey(KeyCode.LeftArrow);
        public bool Right =>
            transform.CompareTag("Player1") ? Input.GetKey(KeyCode.D) : Input.GetKey(KeyCode.RightArrow); 
        public bool Attack => transform.CompareTag("Player1") ? Input.GetKey(KeyCode.J) : Input.GetKey(KeyCode.Keypad1);
    }
}