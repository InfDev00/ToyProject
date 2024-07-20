using System;
using Entities.EntitySubClass;
using UnityEngine;
using Utils;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        // 이동 가능한 모든 물체의 기본 클래스
        [Header("Customizable")] 
        public float initialHealth = 10;
        public float initialDef;
        public float initialVelocity;
        public float initialJumpPower;

        private BoxCollider _collider;
        private Rigidbody _rigidBody;
        
        public EntityMove EntityMove;
        public EntityStatus EntityStatus;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            // 각 Entity는 타인과 충돌 시 발생하는 코드 포함
            var comp = collision.gameObject.GetComponent<Entity>();
            switch (collision.gameObject.tag)
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

        public void BeInvincibility(bool invincible)
        {
            _collider.enabled = !invincible;
            _rigidBody.useGravity = !invincible;
        }
        
        protected EntityMove CreateEntityMove() =>
            new(initialVelocity, initialJumpPower, GetComponent<Rigidbody>(), GetComponent<Transform>());
    }
}
