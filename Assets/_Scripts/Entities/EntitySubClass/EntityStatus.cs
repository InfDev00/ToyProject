using System;

namespace Entities.EntitySubClass
{
    public class EntityStatus
    {
        private float _hp;
        private readonly float _def;

        private readonly Action _onDamaged = null;
        
        public EntityStatus(float initialHealth, float initialDef, Action damagedAction = null)
        {
            _hp = initialHealth;
            _def = initialDef;
            _onDamaged += damagedAction;
        }

        public bool GetDamage(float damage)
        {
            _onDamaged?.Invoke();
            _hp -= damage * (1 - _def * 0.1f / (1 + _def * 0.1f)); // 상수 조절해서 방어력 배율 조절

            return _hp > 0;
        }
    }
}