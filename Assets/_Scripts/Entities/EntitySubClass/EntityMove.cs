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
            var force3d = new Vector3(force2d.x, 0, force2d.y);
            _rigidBody.AddForce(force3d * _velocity * Time.fixedDeltaTime);
        }

        public void Jump() => _rigidBody.AddForce(new Vector3(0, _jumpPower, 0));
    }
}