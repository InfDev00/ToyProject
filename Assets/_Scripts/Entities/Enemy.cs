using System;
using Entities.EntitySubClass;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        public GameObject followTarget;
        
        private void Awake()
        {
            _entityMove = new EntityMove(initialVelocity, initialJumpPower, GetComponent<Rigidbody>());
            _entityStatus = new EntityStatus(initialHealth, initialDef);
        }

        private void FixedUpdate()
        {
            if (followTarget)
            {
                var direction = (followTarget.transform.position - transform.position).normalized;
                var direction2D = new Vector2(direction.x, direction.z);
                _entityMove.Move(direction2D);
            }
        }

        protected override void OnHitEnemy(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnHitPlayer(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnHitObject(GameObject obj)
        {
            throw new System.NotImplementedException();
        }
    }
}