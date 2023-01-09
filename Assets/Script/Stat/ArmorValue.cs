using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorValue : BaseStat
{
    public ArmorValue() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_armor_value;
    }
}
