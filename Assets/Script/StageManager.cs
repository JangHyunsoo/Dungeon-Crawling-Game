using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField]
    private StageData curr_stage_data_;
    public StageData curr_stage_data { get => curr_stage_data_; }

    public void init()
    {
        
    }

    public void startStage()
    {
        
    }
}