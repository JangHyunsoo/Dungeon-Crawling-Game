using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpRegen : BaseStat
{
    public MpRegen() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_mp_regen;
    }
}
