using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableStat : MonoBehaviour
{
    private Dictionary<StatType, Stat> stat_dic_ = new Dictionary<StatType, Stat>();

    public void init()
    {
        stat_dic_.Add(StatType.STRENGTH, new Strenght(StatType.STRENGTH));
        stat_dic_.Add(StatType.INTELLIGENT, new Intelligent(StatType.INTELLIGENT));
        stat_dic_.Add(StatType.DEXTERITY, new Dexterity(StatType.DEXTERITY));
        stat_dic_.Add(StatType.MAX_HP, new MaxHp(StatType.MAX_HP));
        stat_dic_.Add(StatType.MAX_MP, new MaxMp(StatType.MAX_MP));
        stat_dic_.Add(StatType.HP_REGEN, new HpRegen(StatType.HP_REGEN));
        stat_dic_.Add(StatType.MP_REGEN, new MpRegen(StatType.MP_REGEN));
        stat_dic_.Add(StatType.ARMOR_VALUE, new ArmorValue(StatType.ARMOR_VALUE));

        stat_dic_.Add(StatType.PHYSICAL_DAMAGE, new PysicalDamage());
        stat_dic_.Add(StatType.CRITICAL_CHANCE, new CriticalChance());
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
