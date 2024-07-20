using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.VFX;

namespace Entities.Weapons
{
    public class Sword : Weapon, IEnemyHitHandler
    {
        private WeaponManager _manager;
        public VisualEffect visualEffect;

        private void Start()
        {
            _manager = GetComponentInParent<WeaponManager>();
            _colliders = GetComponentsInChildren<BoxCollider>();
            
            SetCollidersEnabled(false);
        }

        protected override void Use()
        {
            _manager.AttackDirection();
            if (visualEffect)
            {
                visualEffect.Play();
                StartCoroutine(DisableColliderAfterEffect());
            }
            SetCollidersEnabled(true);
        }

        private IEnumerator DisableColliderAfterEffect()
        {
            yield return new WaitUntil(() => visualEffect.aliveParticleCount==0);
            
            SetCollidersEnabled(false);
        }

        public void OnHitEnemy(Enemy enemy)
        {
            if(enemy is IDamageable damageable) damageable.Damaged(damage);
        }
    }
}