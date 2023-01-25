using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    [SerializeField]
    private ItemData[] total_item_arr_;
    private Dictionary<ItemType, Dictionary<int, ItemData>> item_type_dic_ = new Dictionary<ItemType, Dictionary<int, ItemData>>();
    private Dictionary<ItemType, Dictionary<int, Dictionary<int, ItemData>>> item_type_rarlity_dic_ = new Dictionary<ItemType, Dictionary<int, Dictionary<int, ItemData>>>();

    [SerializeField]
    private StatValuePair[] random_addition_stat_arr_;

    public ItemData getItemData(int _index)
    {
        return total_item_arr_[_index];
    }

    public ItemData getItemData(ItemType _item_type, int _index)
    {
        return item_type_dic_[_item_type][_index];
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

    public void init()
    {
        foreach (var item_data in total_item_arr_)
        {
            if (!item_type_dic_.ContainsKey(item_data.item_type))
            {
                item_type_dic_[item_data.item_type] = new Dictionary<int, ItemData>();
            }
            item_type_dic_[item_data.item_type][item_data.index_no] = item_data;
        }
    }
}

[System.Serializable]
public struct StatValuePair
{
    public StatType stat_type;
    public int value;
}