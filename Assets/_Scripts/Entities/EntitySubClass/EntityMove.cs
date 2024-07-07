using UnityEngine;

namespace Entities.EntitySubClass
{
    public class EntityMove
    {
        private readonly float _velocity;
        private readonly float _jumpPower;
        
        private readonly Rigidbody _rigidBody;

        public EntityMove(float initialVelocity, float initialJumpPower, Rigidbody rigid)
        {
            _velocity = initialVelocity;
            _jumpPower = initialJumpPower;
            _rigidBody = rigid;
        }

        public void Move(Vector2 force2d)
        {
            if (force2d == Vector2.zero)
            {
                _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0);
                return;
            }

            var force3d = new Vector3(force2d.x, 0, force2d.y);
            var velocity = force3d.normalized * _velocity;
            
            _rigidBody.velocity = new Vector3(velocity.x, _rigidBody.velocity.y, velocity.z);
        }

        public void Jump() => _rigidBody.AddForce(0, _jumpPower * 10, 0);
    }
}