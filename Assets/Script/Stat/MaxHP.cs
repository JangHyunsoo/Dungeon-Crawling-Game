using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHp : BaseStat
{
    public MaxHp() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_hp;
    }
}
