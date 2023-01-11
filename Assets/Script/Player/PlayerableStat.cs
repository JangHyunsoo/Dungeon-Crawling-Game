using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerableStat
{
    private Dictionary<StatType, Stat> stat_dic_ = new Dictionary<StatType, Stat>();

    public void init()
    {
        stat_dic_.Add(StatType.STRENGTH, new Strenght());
        stat_dic_.Add(StatType.INTELLIGENT, new Intelligent());
        stat_dic_.Add(StatType.DEXTERITY, new Dexterity());
        stat_dic_.Add(StatType.MAX_HP, new MaxHp());
        stat_dic_.Add(StatType.MAX_MP, new MaxMp());
        stat_dic_.Add(StatType.HP_REGEN, new HpRegen());
        stat_dic_.Add(StatType.MP_REGEN, new MpRegen());
        stat_dic_.Add(StatType.ARMOR_VALUE, new ArmorValue());

        stat_dic_.Add(StatType.PHYSICAL_DAMAGE, new PysicalDamage());
        stat_dic_.Add(StatType.CRITICAL_CHANCE, new CriticalChance());
        stat_dic_.Add(StatType.CRITICAL_DAMAGE, new CriticalDamage());
        stat_dic_.Add(StatType.ACTION_POINT, new ActionPoint());
        stat_dic_.Add(StatType.HIT_ACCURACY, new HitAccuracy());
        stat_dic_.Add(StatType.CASTING_CHANCE, new CastingChance()); 
    }

    public void addValue(StatType _stat_type, float _value)
    {
        ((BaseStat)stat_dic_[_stat_type]).setAdditionValue(_value);
    }

    public float getValue(StatType _stat_type)
    {
        return stat_dic_[_stat_type].getValue();
    }
}
