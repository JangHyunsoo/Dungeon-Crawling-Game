using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PysicalDamage : Stat
{
    public override float getValue()
    {
        // PlayerableEquipment���� �� �ҷ��� �ʿ� �־�Ӥ�����
        return PlayerManager.instance.playerable_stat.getValue(StatType.STRENGTH);
    }
}
