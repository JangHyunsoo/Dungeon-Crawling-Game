using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : Stat
{
    protected float base_stat_;
    protected float addition_stat_;
    protected float level_per_stat_;

    protected BaseStat()
    {
        addition_stat_ = 0f;
        level_per_stat_ = 0f;
    }

    public override float getValue()
    {
        return base_stat_ + addition_stat_;
    }

    public void setAdditionValue(float _value)
    {
        addition_stat_ += _value;
    }
}

