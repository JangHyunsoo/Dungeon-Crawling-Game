using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamage : Stat
{
    public override float getValue()
    {
        float strenght = PlayerManager.instance.playable.playable_stat.getValue(StatType.STRENGTH);
        WeaponData weapon_data = (WeaponData)PlayerManager.instance.playable.playable_equipment.weapon.item_data;
        float weapon_aptitude_level = PlayerManager.instance.playable.playable_aptitude.aptitude_level_dic[Utility.convertAptitudeTypeToWeaponType(weapon_data.weapon_type)].cur_level;
        return 1f + ((strenght / 20f) + (weapon_aptitude_level / 50f + 0.5f));
    }
}
