using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAccuracy : Stat
{
    public override float getValue()
    {
        return 0f;
        //return PlayerManager.instance.playerable_stat.getValue()
    }
}
