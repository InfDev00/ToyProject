using System;
using Entities;
using UnityEngine;

namespace Managers
{
    public class WeaponManager : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake() => _player = GetComponentInParent<PlayerController>();

        private void FixedUpdate() // 회전 보정
        {
            var playerRotation = _player.transform.rotation.y;
            transform.rotation = Quaternion.Euler(0, -playerRotation, 0);
        }
    }
}