using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalDamage : Stat
{
    public override float getValue()
    {
        float strenght = PlayerManager.instance.playerable_stat.getValue(StatType.STRENGTH);
        WeaponData weapon_data = (WeaponData)PlayerManager.instance.playerable_inventory.playerable_equipment.getWeapon().item_data;
        float weapon_aptitude_level = PlayerManager.instance.playerable_aptitude.aptitude_level_dic[Utility.convertAptitudeTypeToWeaponType(weapon_data.weapon_type)].cur_level;
        return 1f + ((strenght / 20f) + (weapon_aptitude_level / 50f + 0.5f));
    }
}
