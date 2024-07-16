using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Enemy Infos
    // Enemy List or Enemy Spawner

    public Stage(float total, float interval)
    {
        _totalStageTime = total;
        _intervalTime = interval;
    }
}
