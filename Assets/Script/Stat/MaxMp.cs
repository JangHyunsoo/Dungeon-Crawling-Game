using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMp : BaseStat
{
    public MaxMp() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_mp;
    }
}
