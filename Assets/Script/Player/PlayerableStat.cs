using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerableStat
{
    private Dictionary<StatType, Stat> stat_dic_;

    public void initStatDicitionary()
    {
        stat_dic_ = new Dictionary<StatType, Stat>();

        stat_dic_.Add(StatType.STRENGTH, new BaseStat());
        stat_dic_.Add(StatType.INTELLIGENT, new BaseStat());
        stat_dic_.Add(StatType.DEXTERITY, new BaseStat());
        stat_dic_.Add(StatType.MAX_HP, new BaseStat());
        stat_dic_.Add(StatType.MAX_MP, new BaseStat());
        stat_dic_.Add(StatType.HP_REGEN, new BaseStat());
        stat_dic_.Add(StatType.MP_REGEN, new BaseStat());
        stat_dic_.Add(StatType.ARMOR_VALUE, new BaseStat());

        stat_dic_.Add(StatType.PHYSICAL_DAMAGE, new PysicalDamage());
        stat_dic_.Add(StatType.CASTING_CHANCE, new CriticalChance());
        stat_dic_.Add(StatType.CRITICAL_DAMAGE, new CriticalDamage());
        stat_dic_.Add(StatType.ACTION_POINT, new ActionPoint());
        stat_dic_.Add(StatType.HIT_ACCURACY, new HitAccuracy());
        stat_dic_.Add(StatType.CASTING_CHANCE, new CastingChance());
    }

    public float getValue(StatType _stat_type)
    {
        return stat_dic_[_stat_type].getValue();
    }
}
