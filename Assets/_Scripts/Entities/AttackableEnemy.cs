using UnityEngine;

namespace Entities
{
    public class AttackableEnemy : Enemy
    {
        [Header("Attackable")] 
        public float attackRange;
        public Weapon weapon;

        protected override void FixedUpdate()
        {
            if (!IsInRange())
            {
                weapon.stopAttack = true;
            }
            
            base.FixedUpdate();
        }

        private bool IsInRange()
        {
            if (followTarget && Vector3.Distance(followTarget.position, transform.position) < attackRange)
            {
                weapon.stopAttack = false;
                weapon.attackRotation = transform.rotation;

                return true;
            }

            return false;
        }
    }
}