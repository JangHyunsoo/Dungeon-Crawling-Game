using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : Stat
{
    protected float base_stat_;
    protected float addition_stat_;
    protected float level_per_stat_;
    protected StatType stat_type_;


    protected BaseStat(StatType _stat_type)
    {
        stat_type_ = _stat_type;
        addition_stat_ = 0f;
        level_per_stat_ = 0f;
    }

    public override float getValue()
    {
        var playable_equipment = PlayerManager.instance.playable.playable_equipment;

        var weapon_bonus = playable_equipment.weapon.addition_stat_dic[stat_type_];
        var armor_bonus = playable_equipment.armor.addition_stat_dic[stat_type_];

        return 1f;
    }

    public void setAdditionValue(float _value)
    {
        addition_stat_ += _value;
    }
}

