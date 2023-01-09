using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dexterity : BaseStat
{
    public Dexterity() : base()
    {
        base_stat_ = PlayerManager.instance.playerable.playerable_data.base_dexterity;
    }
}
