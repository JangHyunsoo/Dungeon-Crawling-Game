using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : Item
{
    protected Dictionary<StatType, float> addition_stat_dic_ = new Dictionary<StatType, float>();
    public Dictionary<StatType, float> addition_stat_dic { get => addition_stat_dic_; }

    protected int enhanced_value_;
    public int enhanced_value { get => enhanced_value_; }

    private int addition_stat_size_;

    protected bool is_curse_;
    public bool is_curse { get => is_curse_; }

    public bool include_addition_stat { get => addition_stat_dic_.Count != 0; }

    public EquipItem(ItemData _equip_item_data) : base(_equip_item_data)
    {
        item_data_ = _equip_item_data;
        enhanced_value_ = 0;
        setAdditionStatDic();

        if (setIncludeAddition())
        {
            setRandomStat();
        }
    }

    private void setAdditionStatDic()
    {
        addition_stat_dic_[StatType.STRENGTH] = 0f;
        addition_stat_dic_[StatType.INTELLIGENT] = 0f;
        addition_stat_dic_[StatType.DEXTERITY] = 0f;
        addition_stat_dic_[StatType.MAX_HP] = 0f;
        addition_stat_dic_[StatType.MAX_MP] = 0f;
        addition_stat_dic_[StatType.HP_REGEN] = 0f;
        addition_stat_dic_[StatType.MP_REGEN] = 0f;
        addition_stat_dic_[StatType.ARMOR_VALUE] = 0f;
    }

    // 네이밍 수정 요망 > 모호한 이름 
    protected virtual bool setIncludeAddition()
    {
        const int hidden_weapon_chance = 40;
        int random_num = UnityEngine.Random.RandomRange(1, 100);
        return random_num <= hidden_weapon_chance;
    }

    protected virtual void setRandomStat()
    {
        enhanced_value_ = UnityEngine.Random.RandomRange(0, 3);
        int random_stat_size = UnityEngine.Random.RandomRange(1, 4);
        var random_stat_arr = ItemDatabase.instance.getRandomStatTypeArr(random_stat_size);

        for (int i = 0; i < random_stat_size; i++)
        {
            var cur_random_stat_pair = random_stat_arr[i];
            var cur_random_value = UnityEngine.Random.RandomRange(-cur_random_stat_pair.value, cur_random_stat_pair.value);
            addition_stat_dic_[cur_random_stat_pair.stat_type] += cur_random_value;
        }
    }
}
