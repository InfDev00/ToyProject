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
    public static UnityAction<Stage> OnStageChanged;
    // All methods that should be called when Stage Starts
    public UnityEvent OnStageStart;
    // All methods that should be called when Stage Proceeds
    public UnityEvent OnStageInterval;
    // All methods that should be called when Stage Ends
    public UnityEvent OnStageEnd;
    // Current Stage
    public Stage currentStage;


    // Set Current Stage to next Stage
    public void SetStage()
    {
        if(stages.Count==0)
        {
            Debug.Log("You have reached the End Stage. Add more Stages to Proceed.");
            return;
        }
        currentStage = stages[0];
        stages.RemoveAt(0);
    }

    // Set Time to 0
    public void SetTime()
    {
        elapsedTime = 0;
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
        SetStage();
        // Where to Invoke OnStageStart Event? Not here
        //OnStageChanged?.Invoke();
    }

    public void UpdateStage(stageType type, float gamePlayTime, int enemyCnt)
    {
        
        switch (type)
        {
            case stageType.survive:
                UpdateSurviveStage(gamePlayTime);
                break;
            case stageType.genocide:
                UpdateGenocideStage(enemyCnt);
                break;
        }

    }

    private void UpdateSurviveStage(float f)
    {
        if(f < 0)
        {
            SetStage();
            OnStageChanged?.Invoke(currentStage);
        }
    }

    private void UpdateGenocideStage(int i)
    {
        if(i<=0)
        {
            SetStage();
            OnStageChanged?.Invoke(currentStage);
        }
    }

    // �̰� ���� �� ���� ����� ��������? ����
    //void Update()
    //{
    //    if (!_isStageProcess) return;

    //    elapsedTime += Time.deltaTime;
    //    intervalTime += Time.deltaTime;
    //    if (elapsedTime > currentStage.TotalStageTime) 
    //    {
    //        intervalTime = 0;
    //        _isStageProcess = false;
    //        OnStageChanged?.Invoke();
    //    }

    //    if(intervalTime > currentStage.IntervalTime)
    //    {
    //        intervalTime = 0;
    //        OnStageInterval?.Invoke();
    //    }
       
    //}
}
