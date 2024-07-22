using UnityEngine;

namespace Entities.EntitySubClass
{
    public class EntityMove
    {
        private float _velocity;
        private readonly float _jumpPower;
        
        private readonly Rigidbody _rigidBody;
        private readonly Transform _transform;
        
        static readonly float cos = Mathf.Cos(-45f * Mathf.Deg2Rad);
        static readonly float sin = Mathf.Sin(-45f * Mathf.Deg2Rad);

        public EntityMove(float initialVelocity, float initialJumpPower, Rigidbody rigid, Transform transform)
        {
            _velocity = initialVelocity;
            _jumpPower = initialJumpPower;
            _rigidBody = rigid;
            _transform = transform;
        }

        public void UpdateVelocity(float velocity) => _velocity = velocity;

        public void MovePlayer(Vector2 force2d)
        {
            Move(new Vector2(force2d.x * cos - force2d.y * sin, force2d.x * sin + force2d.y * cos));
        }

        public void Move(Vector3 force3d, bool is2d = false)
        {
            var velocity = force3d.normalized * _velocity;

            _rigidBody.velocity =
                is2d
                    ? new Vector3(velocity.x, _rigidBody.velocity.y, velocity.z)
                    : new Vector3(velocity.x, velocity.y, velocity.z);
            Rotation(force3d);
        }
        
        public void Move(Vector2 force2d)
        {
            if (force2d == Vector2.zero)
            {
                _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0);
                return;
            }

            var force3d = new Vector3(force2d.x, 0, force2d.y);
            Move(force3d, true);
        }

        public void Jump() => _rigidBody.AddForce(0, _jumpPower * 10, 0);

        private void Rotation(Vector3 force3d)
        {
            var target = Quaternion.LookRotation(force3d);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, target, Time.deltaTime * 10);
        }

        public void KnockBack(Vector3 target, float power)
        {
            var dist = (_transform.position - target).normalized;
            _rigidBody.AddForce(dist * power);
        }

        public void Stop() => _rigidBody.velocity = Vector3.zero;
    }
}