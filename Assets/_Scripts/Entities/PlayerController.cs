using System;
using Entities.EntitySubClass;
using UI;
using UnityEngine;
using Utils;

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
            EntityMove = new EntityMove(initialVelocity, initialJumpPower, GetComponent<Rigidbody>());
            EntityStatus = new EntityStatus(initialHealth, initialDef);
            gameObject.tag = Tags.PLAYER;
        }

        private void FixedUpdate()
        {
            var force2d = joyStick.InputVector;
            force2d = new Vector2(force2d.x * cos - force2d.y * sin, force2d.x * sin + force2d.y * cos);
            EntityMove.Move(force2d);
        }

        public void Jump() => EntityMove.Jump();


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
    }
}