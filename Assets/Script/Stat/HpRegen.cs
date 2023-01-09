using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRegen : BaseStat
{
    public HpRegen() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_hp_regen;
    }
}
