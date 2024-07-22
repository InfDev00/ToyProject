using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();

    public static UnityAction<Enemy> OnEnemyKilled;

    void EnemyListRemove(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public int UpdateEnemyCount()
    {
        return enemyList.Count;
    }

    private void Awake()
    {
        OnEnemyKilled += EnemyListRemove;
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
