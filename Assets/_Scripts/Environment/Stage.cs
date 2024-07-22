using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stageType { genocide, survive }

[CreateAssetMenu(fileName = "Stage Data", menuName ="Stage Data")]

public class Stage : ScriptableObject
{
    // Informations of current Stage

    // Time Infos
    [SerializeField] float _totalStageTime; 
    public float TotalStageTime 
    { 
        get 
        {
            return _totalStageTime;
        }
    }
    [SerializeField] float _intervalTime; 

    public float IntervalTime
    {
        get 
        { 
            return _intervalTime; 
        }
    }

    public stageType type;

    // Enemy Infos
    // Enemy List or Enemy Spawner

    public List<Enemy> currentStageEnemies = new List<Enemy>();

    public int totalEnemyCount;

    
}
