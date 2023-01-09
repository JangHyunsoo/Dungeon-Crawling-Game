using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strenght : BaseStat
{
    public Strenght() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_strength;
    }
}
