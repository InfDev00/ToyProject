using System;
using Entities.EntitySubClass;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Enemy : Entity
    {
        public GameObject followTarget;
        
        private void Awake()
        {
            EntityMove = new EntityMove(initialVelocity, initialJumpPower, GetComponent<Rigidbody>());
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

        protected override void OnHitEnemy(GameObject obj)
        {

        }

        protected override void OnHitPlayer(GameObject obj)
        {

        }

        protected override void OnHitObject(GameObject obj)
        {

        }
    }
}