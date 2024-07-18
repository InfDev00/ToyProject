using System;
using Entities.EntitySubClass;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Enemy : Entity, IDamageable
    {
        public GameObject followTarget;
        
        private void Awake()
        {
            EntityMove = CreateEntityMove();
            EntityStatus = new EntityStatus(initialHealth, initialDef);
            gameObject.tag = Tags.ENEMY;
        }

        private void FixedUpdate()
        {
            if (followTarget)
            {
                var direction = (followTarget.transform.position - transform.position).normalized;
                var direction2D = new Vector2(direction.x, direction.z);
                EntityMove.Move(direction2D);
            }
        }

        public void Damaged(float damage)
        {
            Debug.Log("Enemy Damaged");
            if(! EntityStatus.GetDamage(damage)) Destroy(gameObject); //test
        }
    }
}