using Entities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> spawnTargets = new List<GameObject>();
    public List<Enemy> enemies;

    [SerializeField] EnemyManager _enemyManager;

    [SerializeField] GameObject _player;

    public void SetEnemyList(Stage stageInfo)
    {
        enemies = stageInfo.currentStageEnemies;
    }

    public void SpawnEnemy(Stage stageInfo)
    {
        for (int i = 0; i < stageInfo.totalEnemyCount; i++)
        {
            int enemyRandInt = Random.Range(0, enemies.Count);
            int pointRandInt = Random.Range(0, spawnTargets.Count);
            Enemy _instantiatedEnemy = Instantiate(enemies[enemyRandInt], spawnTargets[pointRandInt].transform.position, Quaternion.identity);
            _instantiatedEnemy.followTarget = _player.transform;
            _enemyManager.enemyList.Add(_instantiatedEnemy);
        }
        
    }
    
    void Awake()
    {
        StageManager.OnStageChanged += SetEnemyList;

        StageManager.OnStageChanged += SpawnEnemy;
    }

    private void OnDestroy()
    {
        StageManager.OnStageChanged -= SetEnemyList;

        StageManager.OnStageChanged -= SpawnEnemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
