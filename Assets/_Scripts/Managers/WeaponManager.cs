using System;
using Entities;
using UnityEngine;

namespace Managers
{
    public class WeaponManager : MonoBehaviour
    {
        private PlayerController _player;
        private float _attack = 0;

        private void Awake() => _player = GetComponentInParent<PlayerController>();

        private void FixedUpdate()
        {
            var playerRotation = _player.transform.rotation.y;
            transform.rotation = Quaternion.Euler(0, -playerRotation + _attack, 0);
        }

        public void AttackDirection()
        {
            _attack = _player.transform.rotation.eulerAngles.y;
        }
    }
}