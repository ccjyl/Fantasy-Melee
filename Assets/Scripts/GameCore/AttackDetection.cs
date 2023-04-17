using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FantasyMelee
{
    /// <summary>
    /// 攻击检测
    /// Author:clof
    /// </summary>
    public class AttackDetection : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                playerController.underAttack = true;
                playerController.attacker = _playerController;//攻击者
                _playerController.atkTarget = playerController;//攻击目标
            }
        }
    }
}
