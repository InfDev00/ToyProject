using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.VFX;

namespace Entities.Weapons
{
    public class Sword : Weapon, IEnemyHitHandler
    {
        public VisualEffect visualEffect;

        private void Start()
        {
            visualEffect.Stop();
            _colliders = GetComponentsInChildren<BoxCollider>();
            
            Debug.Log(visualEffect.gameObject.name);
            
            SetCollidersEnabled(false);
        }

        protected override void Use()
        {
            if (visualEffect)
            {
                var rot = pointer.transform.rotation.eulerAngles.y;
                visualEffect.transform.rotation = Quaternion.Euler(0, rot + 90, 0);
                
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