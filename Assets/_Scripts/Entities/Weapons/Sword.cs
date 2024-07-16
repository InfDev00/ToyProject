using System.Collections;
using UnityEngine;
using Utils;

namespace Entities.Weapons
{
    public class Sword : Weapon, IEnemyHitHandler
    {
        protected override void Use()
        {
            StopCoroutine(Swing());
            StartCoroutine(Swing());
        }

        private IEnumerator Swing()
        {
            weaponObject.SetActive(true);
            anim.SetFloat(Values.ANIM_ATTACK_SPEED, 1/attackSpeed);
            anim.SetTrigger(Values.ANIM_ATTACK);
            yield return new WaitForSeconds(attackSpeed);
            weaponObject.SetActive(false);
        }

        public void OnHitEnemy(Enemy enemy)
        {
            if(enemy is IDamageable damageable) damageable.Damaged(1);
        }
    }
}