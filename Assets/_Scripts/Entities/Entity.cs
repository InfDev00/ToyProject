using System;
using Entities.EntitySubClass;
using UnityEngine;

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
        
        protected EntityMove _entityMove;
        protected EntityStatus _entityStatus;

        protected void Hit(GameObject obj)
        {
            switch (obj.tag)
            {
                case "Enemy":
                    OnHitEnemy(obj);
                    break;
                case "Player":
                    OnHitPlayer(obj);
                    break;
                case "Object":
                    OnHitObject(obj);
                    break;
            }
        }

        protected abstract void OnHitEnemy(GameObject obj);
        protected abstract void OnHitPlayer(GameObject obj);
        protected abstract void OnHitObject(GameObject obj);
    }
}
