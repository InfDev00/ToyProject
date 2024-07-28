using System.Collections;
using UnityEngine;

namespace Entities.Weapons
{
    public class Gun : Weapon
    {
        [Header("Gun Attack")]
        public GameObject bulletPrefab;
        public float bulletDelay;
        public int bulletCount;
        public float bulletVelocity;
        protected override void Use()
        {
            var dir3d = attackRotation * Vector3.forward;
            var dir2d = new Vector2(dir3d.x, dir3d.z);

            StartCoroutine(Shoot(dir2d));
        }

        private IEnumerator Shoot(Vector2 dir)
        {
            for (var i = 0; i < bulletCount; ++i)
            {
                InstantiateBullet(dir);
                yield return new WaitForSeconds(bulletDelay);
            }
        }
        
        private void InstantiateBullet(Vector2 dir)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
            bullet.Init(damage, bulletVelocity, dir, BulletTarget.PLAYER);
        }
    }
}