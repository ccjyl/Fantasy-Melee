using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 角色控制基类
    /// Author:clof
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        //引用
        [HideInInspector] public InputMode inputMode; //输入方式
        private Rigidbody2D _rigidbody2D;

        public PlayerType currentPlayerType; //当前玩家

        [Header("数据"), Tooltip("角色数据")] public PlayerData playerData;
        [Header("地面检测"), SerializeField, Tooltip("检测点中心")] private Transform groundDetectionCenter;
        [SerializeField, Tooltip("检测层级")] private LayerMask groundDetectionLayer;
        [SerializeField, Tooltip("检测范围")] private float groundDetectionRadius = 0.2f;


        protected virtual void Awake()
        {
            inputMode = GetComponent<InputMode>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        #region 移动

        public void Move(float speed)
        {
            if (inputMode.Move)
            {
                transform.localScale = new Vector3(inputMode.AxisX, 1f, 1f);
            }

            SetVelocityX(speed * inputMode.AxisX);
        }

        /// <summary>
        /// 设置玩家刚体速度
        /// </summary>
        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        /// <summary>
        /// 设置刚体x轴速度
        /// </summary>
        public void SetVelocityX(float velocityX)
        {
            _rigidbody2D.velocity = new Vector2(velocityX, _rigidbody2D.velocity.y);
        }

        /// <summary>
        /// 设置刚体y轴速度
        /// </summary>
        public void SetVelocityY(float velocityY)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocityY);
        }

        /// <summary>
        /// 设置重力 0不开启 1开启
        /// </summary>
        public void SetUseGravity(float value)
        {
            _rigidbody2D.gravityScale = value;
        }

        #endregion

        #region 地面检测

        public bool IsGrounded => GroundDetection() && _rigidbody2D.velocity.y < 0.1f;

        private bool GroundDetection()
        {
            return Physics2D.OverlapCircle(groundDetectionCenter.position, groundDetectionRadius, groundDetectionLayer);
        }

        /// <summary>
        /// 绘制地面检测圆
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundDetectionCenter.position, groundDetectionRadius);
        }

        #endregion

        #region 攻击

        /// <summary>
        /// 按键预输入
        /// </summary>
        /// <param name="delayTime">预输入时间</param>
        protected IEnumerator Preinput(float delayTime, bool delayName)
        {
            delayName = true;
            yield return new WaitForSeconds(delayTime);
            delayName = false;
        }

        #endregion
    }

    /// <summary>
    /// one表示当前为玩家1 控制
    /// Two表示当前为玩家2 控制
    /// </summary>
    public enum PlayerType
    {
        One,
        Two
    }
}