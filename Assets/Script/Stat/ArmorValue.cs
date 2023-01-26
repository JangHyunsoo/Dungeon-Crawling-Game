using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorValue : BaseStat
{
    public ArmorValue(StatType _stat_type) : base(_stat_type)
    {
        base_stat_ = PlayerManager.instance.playable.playable_data.base_armor_value;
    }
}
