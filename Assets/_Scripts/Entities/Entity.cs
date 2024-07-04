using System;
using Entities.EntitySubClass;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour
    {
        // 상호작용 가능한 모든 물체의 기본 클래스
        [Header("Customizable")]
        public float initialVelocity;
        public float initialJumpPower;
        
        protected EntityMove _entityMove;
        // 추후 다른 이동 기능도 추가 예정

        protected void CreateEntityMove() => _entityMove = new EntityMove(initialVelocity,initialJumpPower,this.GetOrAddComponent<Rigidbody>());
    }
}
