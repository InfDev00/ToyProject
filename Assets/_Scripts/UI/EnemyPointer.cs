using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UI
{
    public class EnemyPointer : MonoBehaviour
    {
        public Transform player;
        public float detectionRadius = 50f;
        public int maxEnemies = 100;
        
        private readonly List<Vector3> _enemiesInRange = new List<Vector3>();  
        
        private void FixedUpdate()
        {
            UpdateEnemiesInRange();
            var nearestEnemyPosition = FindNearestEnemy();
            if (nearestEnemyPosition.HasValue)
            {
                RotateArrowTowardsEnemy(nearestEnemyPosition.Value);
            }
        }

        private void UpdateEnemiesInRange()
        {            
            var hitColliders = new Collider[maxEnemies];
            _enemiesInRange.Clear(); 
            int numColliders = Physics.OverlapSphereNonAlloc(player.position, detectionRadius, hitColliders, LayerMask.GetMask(Tags.ENEMY));
        
            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i] != null)
                {
                    _enemiesInRange.Add(hitColliders[i].transform.position);
                }
            }
        }

        
        private Vector3? FindNearestEnemy()
        {
            var closestDistance = Mathf.Infinity;
            Vector3? nearestEnemy = null;

            foreach (var enemyPosition in _enemiesInRange)
            {
                var distance = Vector3.Distance(player.position, enemyPosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemyPosition;
                }
            }

            return nearestEnemy;
        }

        private void RotateArrowTowardsEnemy(Vector3 position)
        {
            var direction = position - player.position;
            direction.y = 0; // 수평 방향만 고려

            var rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
            transform.rotation = rotation;
        }
    }
}