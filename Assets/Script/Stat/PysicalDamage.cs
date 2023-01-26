using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PysicalDamage : Stat
{
    public override float getValue()
    {
        var player_stat = PlayerManager.instance.playable.playable_stat;
        var weapon = PlayerManager.instance.playable.playable_equipment.weapon;
        return player_stat.getValue(StatType.STRENGTH) + ((WeaponData)weapon.item_data).base_damage + weapon.enhanced_value;
    }
}
