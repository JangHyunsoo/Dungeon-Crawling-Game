using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : EquipItem
{
    public Armor(ItemData _equip_item_data) : base(_equip_item_data) { }

    // 네이밍 수정 요망 > 모호한 이름 
    protected override bool setIncludeAddition()
    {
        const int hidden_weapon_chance = 40;
        int random_num = UnityEngine.Random.RandomRange(1, 100);

        return random_num <= hidden_weapon_chance;
    }

    protected override void setRandomStat()
    {
        int random_stat_size = UnityEngine.Random.RandomRange(1, 4);

        var random_stat_arr = ItemDatabase.instance.getRandomStatTypeArr(random_stat_size);

        for (int i = 0; i < random_stat_size; i++)
        {
            random_stat_arr[i].value = UnityEngine.Random.RandomRange(-random_stat_arr[i].value, random_stat_arr[i].value);
            addition_stat_list_.Add(random_stat_arr[i]);
        }
    }
}
