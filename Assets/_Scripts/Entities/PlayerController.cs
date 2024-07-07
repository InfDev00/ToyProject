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
        readonly float cos = Mathf.Cos(-45f * Mathf.Deg2Rad);
        readonly float sin = Mathf.Sin(-45f * Mathf.Deg2Rad);
        private void Awake()
        {
            _entityMove = new EntityMove(initialVelocity, initialJumpPower, GetComponent<Rigidbody>());
            _entityStatus = new EntityStatus(initialHealth, initialDef);
        }

        private void FixedUpdate()
        {
            var force2d = joyStick.InputVector;
            force2d = new Vector2(force2d.x * cos - force2d.y * sin, force2d.x * sin + force2d.y * cos);
            _entityMove.Move(force2d);
        }

        public void Jump() => _entityMove.Jump();


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