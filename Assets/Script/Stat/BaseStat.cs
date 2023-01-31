using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : Stat
{
    protected float base_stat_;
    protected StatType stat_type_;


    protected BaseStat(StatType _stat_type)
    {
        stat_type_ = _stat_type;
    }

    public override float getValue()
    {
        var playable_equipment = PlayerManager.instance.playable.playable_equipment;

        var weapon_bonus = playable_equipment.weapon.addition_stat_dic[stat_type_];
        var armor_bonus = playable_equipment.armor.addition_stat_dic[stat_type_];
        var ring_bonus = playable_equipment.ring_arr[0].addition_stat_dic[stat_type_] + playable_equipment.ring_arr[1].addition_stat_dic[stat_type_];

        return weapon_bonus + armor_bonus + ring_bonus;
    }
}

