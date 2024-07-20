using Entities.EntitySubClass;
using UI;
using UnityEngine;
using Utils;

namespace Entities
{
    public class PlayerController : Entity, IEnemyHitHandler, IInteractObjectHitHandler
    {
        private bool _isJump;
        public int maxWeapons;
        private Weapon[] _weapons;
        private int _weaponIdx = 0;
        
        private BoxCollider _collider;
        private Rigidbody _rigidBody;
        
        [Header("Child Object")]
        public EnemyPointer pointer;
        public GameObject weaponObj;
        
        private void Awake()
        {
            EntityMove = CreateEntityMove();
            EntityStatus = new EntityStatus(initialHealth, initialDef);
            gameObject.tag = Tags.PLAYER;
            
            _collider = GetComponent<BoxCollider>();
            _rigidBody = GetComponent<Rigidbody>();
            _weapons = new Weapon[maxWeapons];
        }

        public void Jump()
        {
            if (!_isJump) return;
            EntityMove.Jump();
            _isJump = false;
        }

        public void OnHitEnemy(Enemy enemy)
        {
            //enemy.EntityMove.KnockBack(transform.position, 4000);
            Debug.Log("Collision to Enemy");
        }

        public void OnHitInteractObject(InteractObject interact)
        {
            Debug.Log("Collision to interact");
            interact.Interact(gameObject);
        }

        public void AddWeapon(GameObject weaponPrefab)
        {
            var weapon = weaponPrefab.GetComponent<Weapon>();
            
            if (_weaponIdx < maxWeapons)
            {
                Instantiate(weaponPrefab, weaponObj.transform);
                weapon.pointer = pointer;
                _weapons[_weaponIdx++] = weapon;
            }
        }
        
        public void BeInvincibility(bool invincible)
        {
            _collider.enabled = !invincible;
            _rigidBody.useGravity = !invincible;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.GROUND) || other.CompareTag(Tags.ENEMY)) _isJump = true;
        }
    }
}