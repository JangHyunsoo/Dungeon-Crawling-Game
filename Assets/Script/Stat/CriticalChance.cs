using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalChance : Stat
{
    public override float getValue()
    {
        float dexterity = PlayerManager.instance.playerable_stat.getValue(StatType.STRENGTH);
        WeaponData weapon_data = (WeaponData)PlayerManager.instance.playerable_inventory.playerable_equipment.getWeapon().item_data;
        float weapon_aptitude_level = PlayerManager.instance.playerable_aptitude.aptitude_level_dic[Utility.convertAptitudeTypeToWeaponType(weapon_data.weapon_type)].cur_level;
        return Mathf.Round(((dexterity * 0.02f) + (weapon_aptitude_level / 0.01f)) * 10f) / 10f;
    }
}
