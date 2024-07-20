using UnityEngine;
using Utils;

namespace Entities
{
    public abstract class Weapon : MonoBehaviour
    {
        public enum WeaponType
        {
            RANGE_ATTACK,
            MELEE_ATTACK,
        }
        
        [Header("Weapon Status")] 
        public int level = 1;
        public float damage;
        public float attackCoolTime;
        private float _attackDuration;
        
        public WeaponType weaponType;
        public BoxCollider[] _colliders;
        
        protected virtual void Awake()
        {
            _attackDuration = attackCoolTime - 0.5f;
        }

        private void FixedUpdate()
        {
            _attackDuration += Time.fixedDeltaTime;
            if (_attackDuration >= attackCoolTime)
            {
                Use();
                _attackDuration = 0;
            }
        }

        protected abstract void Use();
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            // 각 Entity는 타인과 충돌 시 발생하는 코드 포함
            var comp = other.gameObject.GetComponent<Entity>();
            switch (other.gameObject.tag)
            {
                case Tags.PLAYER:
                    if (this is IPlayerHitHandler playerHitHandler)
                        playerHitHandler.OnHitPlayer(comp as PlayerController);
                    break;
                case Tags.ENEMY:
                    if (this is IEnemyHitHandler enemyHitHandler)
                        enemyHitHandler.OnHitEnemy(comp as Enemy);
                    break;
                case Tags.OBJECT:
                    if(this is IInteractObjectHitHandler interactObjectHitHandler)
                        interactObjectHitHandler.OnHitInteractObject(comp as InteractObject);
                    break;
            }
        }
        
        protected void SetCollidersEnabled(bool isEnabled)
        {
            for (int i = 0; i < _colliders.Length; ++i) _colliders[i].enabled = isEnabled;
        }
    }
}