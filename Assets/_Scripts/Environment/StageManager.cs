using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
    // Variable to check total elapsed Time in current stage
    public float elapsedTime;

    float intervalTime;

    public List<Stage> stages = new List<Stage>();

    // All methods that should be called when Stage Changes
    public UnityEvent OnStageChanged;
    // All methods that should be called when Stage Starts
    public UnityEvent OnStageStart;
    // All methods that should be called when Stage Proceeds
    public UnityEvent OnStageInterval;
    // All methods that should be called when Stage Ends
    public UnityEvent OnStageEnd;
    // Current Stage
    public Stage currentStage;

    bool _isStageProcess;


    // Set Current Stage to next Stage
    public void SetStage()
    {
        
        currentStage = stages[0];
        stages.RemoveAt(0);
    }

    // Set Time to 0
    public void SetTime()
    {
        elapsedTime = 0;
        _isStageProcess = true;
    }

    public void EndStage()
    {
        Debug.Log("Stage Finished");
        //Dosth
    }

    public void IntervalStage()
    {
        Debug.Log("Interval Time Has Passed");
    }
    
    private void Awake()
    {
        // Where to Invoke OnStageStart Event? Not here
        OnStageChanged?.Invoke();
    }

    // 이거 말고 더 좋은 방식이 없으려나? ㅅㅂ
    void Update()
    {
        if (!_isStageProcess) return;

        elapsedTime += Time.deltaTime;
        intervalTime += Time.deltaTime;
        if (elapsedTime > currentStage.TotalStageTime) 
        {
            intervalTime = 0;
            _isStageProcess = false;
            OnStageChanged?.Invoke();
        }

        if(intervalTime > currentStage.IntervalTime)
        {
            intervalTime = 0;
            OnStageInterval?.Invoke();
        }
       
    }
}
