using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PysicalDamage : Stat
{
    public override float getValue()
    {
        // PlayerableEquipment에서 값 불러올 필요 있어보임ㅋㅋㅋ
        return PlayerManager.instance.playerable_stat.getValue(StatType.STRENGTH);
    }
}
