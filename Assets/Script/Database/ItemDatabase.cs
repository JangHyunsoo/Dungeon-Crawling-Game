using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    [SerializeField]
    private ItemData[] item_arr_;

    [SerializeField]
    private StatValuePair[] random_addition_stat_arr_;

    public ItemData getItemData(ItemType _item_type, int _index)
    {
        return item_arr_[_index];
    }
    
    public StatValuePair[] getRandomStatTypeArr(int _num)
    {
        StatValuePair[] seleted_stat_type_arr = new StatValuePair[random_addition_stat_arr_.Length];
        var random_index_arr = Utility.getShuffleArray(random_addition_stat_arr_.Length);

        for (int i = 0; i < _num; i++)
        {
            seleted_stat_type_arr[i] = random_addition_stat_arr_[random_index_arr[i]];
        }
        return seleted_stat_type_arr;
    }
}

[System.Serializable]
public struct StatValuePair
{
    public StatType stat_type;
    public int value;
}
