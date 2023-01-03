using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    private List<StatValuePair> addition_stat_list_;
    public List<StatValuePair> addition_stat_list { get => addition_stat_list_; }

    private int addition_stat_size_;

    private bool is_curse_;
    public bool is_curse { get => is_curse_; }

    public bool include_addition_stat { get => addition_stat_list_.Count != 0; }

    public Weapon()
    {
        if(setIncludeAddition())
        {
            setRandomStat();
        }
    }

    // 네이밍 수정 요망 > 모호한 이름 
    private bool setIncludeAddition()
    {
        const int hidden_weapon_chance = 40;
        int random_num = UnityEngine.Random.RandomRange(1, 100);

        return random_num <= hidden_weapon_chance;
    }
    
    private void setRandomStat()
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
