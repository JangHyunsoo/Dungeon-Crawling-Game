using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMp : BaseStat
{
    public MaxMp(StatType _stat_type) : base(_stat_type)
    {
        base_stat_ = PlayerManager.instance.playable.playable_data.base_mp;
    }
}
