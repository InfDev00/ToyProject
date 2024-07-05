using System;
using Entities.EntitySubClass;
using UI;
using UnityEngine;

namespace Entities
{
    public class PlayerController : Entity
    {
        [Header("Input")]
        public JoyStick joyStick;
        private void Awake()
        {
            _entityMove = new EntityMove(initialVelocity, initialJumpPower, GetComponent<Rigidbody>());
            _entityStatus = new EntityStatus(initialHealth, initialDef);
        }

        private void FixedUpdate()
        {
            _entityMove.Move(joyStick.InputVector);
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
        }
        private void OnCollisionEnter(Collision other)
        {
            Hit(other.gameObject);
        }


    }
}