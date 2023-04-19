using System;
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
        private Rigidbody2D _rigidbody2D;
        [HideInInspector] public InputMode inputMode; //输入方式

        [HideInInspector] public bool underAttack; //受到攻击
        [HideInInspector] public PlayerController attacker; //攻击者
        [HideInInspector] public PlayerController atkTarget; //攻击目标
        [HideInInspector] public bool isLevitate; //是否浮空
        [HideInInspector] public bool isAtk1; //是Atk1
        [HideInInspector] public bool isAtk2; //是Atk2
        [HideInInspector] public bool isSkill1; //是Skill1
        [HideInInspector] public bool isCanParry = true; //能否格挡
        [HideInInspector] public bool isCanHit = true; //能否受伤
        [HideInInspector] public bool isHitting; //受伤中
        [HideInInspector] public float hitCount; //受伤计数
        [HideInInspector] public bool isDoubleJump = false; //是否是蓄力跳
        [HideInInspector] public float currentDamage; //当前伤害值
        [HideInInspector] public bool isImprison; //是否禁锢

        [Header("数据"), Tooltip("角色数据")] public PlayerData playerDataTemplate;
        [HideInInspector] public PlayerData playerData; //角色数据
        [Header("地面检测"), SerializeField, Tooltip("检测点中心")] private Transform groundDetectionCenter;
        [SerializeField, Tooltip("检测层级")] private LayerMask groundDetectionLayer;
        [SerializeField, Tooltip("检测范围")] private float groundDetectionRadius = 0.2f;
        [Header("攻击"), Tooltip("浮空位置")] public Transform levitatePosition;

        private float _currentHp;
        private float _currentMp;
        private WaitForSeconds _autoRecoverMpTime;
        private WaitForSeconds _parryRecoverTime;
        private WaitForSeconds _invincibleTime;
        private WaitForSeconds _resetHitTime;
        private bool _resetHitOpen; //重置协程是否已经启动
        private float _jumpTimer; //跳跃计时器
        private float _imprisonTimer = 0; //解除禁锢计时器

        #region 属性

        /// <summary>
        /// 当前Hp
        /// </summary>
        public float CurrentHp
        {
            get => _currentHp;
            set => _currentHp = Mathf.Clamp(value, 0, playerData.maxHp);
        }

        /// <summary>
        /// 当前Mp
        /// </summary>
        public float CurrentMp
        {
            get => _currentMp;
            set => _currentMp = Mathf.Clamp(value, 0, playerData.maxMp);
        }

        #endregion


        protected virtual void Awake()
        {
            inputMode = GetComponent<InputMode>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            playerData = Instantiate(playerDataTemplate);
            _autoRecoverMpTime = new WaitForSeconds(playerData.recoverMpTime);
            _parryRecoverTime = new WaitForSeconds(playerData.parryCoolTime);
            _invincibleTime = new WaitForSeconds(playerData.invincibleDurationTime);
            _resetHitTime = new WaitForSeconds(playerData.resetHitTime);
        }

        private void OnEnable()
        {
            //赋值
            _currentHp = playerData.maxHp;
            _currentMp = playerData.maxMp;
            StartCoroutine(nameof(AutoRecoverMp)); //自动回蓝
            isCanParry = true;
            isCanHit = true;
            isDoubleJump = false;
            hitCount = 0;
        }

        private void Update()
        {
            if (!isCanParry)
            {
                StartCoroutine(nameof(RecoverParry));
            }

            if (inputMode.JumpOn && IsGrounded)
            {
                StartCoroutine(nameof(DoubleJump));
            }

            if (hitCount - 6 >= 0)
            {
                hitCount = 0;
                isCanHit = false;
                StartCoroutine(nameof(HitInvincible));
            }

            if (isImprison && _imprisonTimer == 0)
            {
                StartCoroutine(nameof(RelieveImprison));
            }
        }


        private void OnDisable()
        {
            StopAllCoroutines();
        }

        #region 状态复制

        /// <summary>
        /// 状态复制
        /// </summary>
        /// <param name="states"></param>
        /// <returns></returns>
        public CharacterStateBase[] copyStateBases(CharacterStateBase[] states)
        {
            CharacterStateBase[] copy = new CharacterStateBase[states.Length];
            for (int i = 0; i < states.Length; i++)
            {
                copy[i] = Instantiate(states[i]);
            }

            return copy;
        }

        #endregion

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
            //背禁锢限制移动
            if (isImprison)
            {
                _rigidbody2D.velocity = Vector2.zero;

                return;
            }

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

        /// <summary>
        /// 设置面朝方向
        /// </summary>
        /// <param name="dir"></param>
        public void SetFaceDirection(float dir)
        {
            transform.localScale = new Vector3(dir, 1f, 1f);
        }

        #endregion

        #region 跳跃蓄力

        IEnumerator DoubleJump()
        {
            _jumpTimer = 0;
            while (!inputMode.Jump)
            {
                _jumpTimer += Time.deltaTime;
                if (_jumpTimer >= 1)
                {
                    isDoubleJump = true;
                }

                yield return null;
            }
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
        /// 能否使用技能
        /// </summary>
        /// <param name="expendMp">该技能消耗的mp</param>
        /// <returns></returns>
        public bool IsUseSkill(float expendMp)
        {
            return CurrentMp >= expendMp;
        }


        /// <summary>
        /// 设置伤害
        /// </summary>
        /// <param name="damage"></param>
        private void OnAnimationDamage_AE(float damage)
        {
            currentDamage = damage;
        }

        #endregion

        #region 自动回复蓝量

        /// <summary>
        /// 每秒回复1点蓝量
        /// </summary>
        IEnumerator AutoRecoverMp()
        {
            while (true)
            {
                CurrentMp += playerData.recoverMp;
                yield return _autoRecoverMpTime;
            }
        }

        #endregion

        #region 格挡

        /// <summary>
        /// 格挡冷却
        /// </summary>
        IEnumerator RecoverParry()
        {
            yield return _parryRecoverTime;
            isCanParry = true;
        }

        #endregion

        #region 受伤

        /// <summary>
        /// hit 无敌
        /// </summary>
        /// <returns></returns>
        IEnumerator HitInvincible()
        {
            yield return _invincibleTime;
            isCanHit = true;
        }

        public void ResetHit()
        {
            StopCoroutine(nameof(ResetHitCount));
            StartCoroutine(nameof(ResetHitCount));
        }

        /// <summary>
        /// 重置hit计数
        /// </summary>
        /// <returns></returns>
        IEnumerator ResetHitCount()
        {
            yield return _resetHitTime;
            hitCount = 0;
        }

        #endregion

        #region 提示

        private void OnGUI()
        {
            Rect rect1 = new Rect(10, 15, 200, 400); //显示位置和大小
            Rect rect2 = new Rect(10, 60, 200, 400); //显示位置和大小
            Rect rect3 = new Rect(400, 0, 200, 400); //显示位置和大小
            Rect rect4 = new Rect(700, 0, 200, 400); //显示位置和大小
            string message1 = "玩家1：a d移动 s防御 w+j上挑\n" +
                              "j攻击 k跳跃 l冲刺\n" +
                              $"技能1:h mp:{playerData.skill1ExpendMp}; 技能2:b mp:{playerData.skill2ExpendMp}; 技能3:n mp:{playerData.skill3ExpendMp}";
            string message2 = "玩家2：← →移动 ↓防御 ↑+1上挑\n" +
                              "1攻击 2跳跃 3冲刺\n" +
                              $"技能1:4 mp:{playerData.skill1ExpendMp}; 技能2:5 mp:{playerData.skill2ExpendMp}; 技能3:6 mp:{playerData.skill3ExpendMp}";
            string message3 = "player1:当前生命值：" + _currentHp + " Mp:" + _currentMp;
            GUIStyle style = new GUIStyle(); //设置样式
            style.fontSize = 12;
            style.fontStyle = FontStyle.Normal;


            GUI.Label(rect1, message1, style);
            GUI.Label(rect2, message2, style);

            if (transform.CompareTag("Player1"))
            {
                GUI.Label(rect3, message3, style);
            }

            if (transform.CompareTag("Player2"))
            {
                GUI.Label(rect4, message3, style);
            }
        }

        #endregion

        #region 禁锢

        /// <summary>
        /// 解除禁锢
        /// </summary>
        /// <returns></returns>
        IEnumerator RelieveImprison()
        {
            while (_imprisonTimer < playerData.skill1ImprisonTime)
            {
                _imprisonTimer += Time.deltaTime;
                yield return null;
            }

            isImprison = false;
            _imprisonTimer = 0;
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