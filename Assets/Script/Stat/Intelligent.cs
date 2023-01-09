using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intelligent : BaseStat 
{
    public Intelligent() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_intelligent;
    }
}
