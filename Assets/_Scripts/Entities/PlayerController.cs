using System;
using Entities.EntitySubClass;
using UI;
using UnityEngine;
using Utils;

namespace Entities
{
    public class PlayerController : Entity
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


        protected override void OnHitEnemy(GameObject obj)
        {
            Debug.Log("Collision to Enemy");
        }

        protected override void OnHitPlayer(GameObject obj)
        {
            throw new NotImplementedException();
        }

        protected override void OnHitObject(GameObject obj)
        {
            Debug.Log("Collision to obj");
            var interact =  obj.GetComponent<InteractObject>();
            
            interact.Interact(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Terrain" || other.CompareTag(Tags.ENEMY)) _isJump = true;
        }
    }
}