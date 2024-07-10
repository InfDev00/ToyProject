using System;
using Entities.EntitySubClass;
using UI;
using UnityEngine;
using Utils;

namespace Entities
{
    public class PlayerController : Entity, IEnemyHitHandler, IInteractObjectHitHandler
    {
        private bool _isJump;
        private void Awake()
        {
            EntityMove = new EntityMove(initialVelocity, initialJumpPower, GetComponent<Rigidbody>());
            EntityStatus = new EntityStatus(initialHealth, initialDef);
            gameObject.tag = Tags.PLAYER;
        }

        public void Jump()
        {
            if (!_isJump) return;
            EntityMove.Jump();
            _isJump = false;
        }

        public void OnHitEnemy(Enemy enemy)
        {
            Debug.Log("Collision to Enemy");
        }

        public void OnHitInteractObject(InteractObject interact)
        {
            Debug.Log("Collision to interact");
            interact.Interact(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Terrain" || other.CompareTag(Tags.ENEMY)) _isJump = true;
        }
    }
}