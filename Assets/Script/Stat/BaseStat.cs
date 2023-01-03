using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : Stat
{
    private float base_stat_;
    private float addition_stat_;
    private float level_per_stat_;

    public BaseStat()
    {
        base_stat_ = 0f;
        addition_stat_ = 0f;
        level_per_stat_ = 0f;
    }

    public override void init()
    {
    }

    public override float getValue()
    {
        return base_stat_ + addition_stat_;
    }
}

