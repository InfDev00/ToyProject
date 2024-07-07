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
        
        public EntityMove EntityMove;
        public EntityStatus EntityStatus;

        protected void Hit(GameObject obj)
        {
            //때린 쪽에서 충돌 판정 실행.
            switch (obj.tag)
            {
                case Tags.ENEMY:
                    OnHitEnemy(obj);
                    break;
                case Tags.PLAYER:
                    OnHitPlayer(obj);
                    break;
                case Tags.OBJECT:
                    OnHitObject(obj);
                    break;
            }
        }

        protected abstract void OnHitEnemy(GameObject obj);
        protected abstract void OnHitPlayer(GameObject obj);
        protected abstract void OnHitObject(GameObject obj);
        
        protected void OnCollisionEnter(Collision other) => Hit(other.gameObject);
    }
}
