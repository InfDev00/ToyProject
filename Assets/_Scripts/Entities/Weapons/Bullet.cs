using UnityEngine;
using Utils;

namespace Entities.Weapons
{       
    public enum BulletTarget  
    {
        PLAYER,
        ENEMY
    }
    public class Bullet : Entity
    {
        private Vector2 _moveDirection;
        public BulletTarget target;
        public float lifeTime = 1f;
        private float _life;
        
        
        private float _damage;

        public void Init(float damage, float velocity, Vector2 dir, BulletTarget bulletTarget)
        {
            _damage = damage;
            initialVelocity = velocity;
            _moveDirection = dir;
            target = bulletTarget;
            EntityMove = CreateEntityMove();
        }
        private void FixedUpdate()
        {
            EntityMove?.Move(_moveDirection);
            
            _life += Time.fixedDeltaTime;
            if(_life > lifeTime) Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            var entity = other.GetComponent<Entity>();
            if ((other.CompareTag(Tags.PLAYER) && target == BulletTarget.PLAYER) ||
                (other.CompareTag(Tags.ENEMY) && target == BulletTarget.ENEMY)) Hit(entity);
        }

        private void Hit(Entity entity)
        {
            if (entity is IDamageable damageable) damageable.Damaged(_damage);
            Destroy(gameObject);
        }
    }
}