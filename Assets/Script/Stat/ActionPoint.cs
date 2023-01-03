using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : Stat
{
    public override float getValue()
    {
        return PlayerManager.instance.playerable_stat.getValue(StatType.DEXTERITY);
    }
}
