using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingChance : Stat
{
    public override float getValue()
    {
        //  int             많이 영향,
        //  magic_aptitude  조금 영향
        //  아마 스펠 수준에 따라 영향을 받기 때문에 Stat에서 빠질 예정임
        return PlayerManager.instance.playable.playable_stat.getValue(StatType.INTELLIGENT);
    }
}
