using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : Singleton<ItemDatabase>
{
    [SerializeField]
    private ItemData[] item_total_arr_;
    private Dictionary<ItemType, List<ItemData>> item_type_dic_ = new Dictionary<ItemType, List<ItemData>>();
    private Dictionary<int, List<ItemData>> item_total_rarlity_dic_ = new Dictionary<int, List<ItemData>>();
    private Dictionary<ItemType, Dictionary<int, List<ItemData>>> item_type_rarlity_dic_ = new Dictionary<ItemType, Dictionary<int, List<ItemData>>>();

    [SerializeField]
    private StatValuePair[] random_addition_stat_arr_;

    public ItemData getItemData(int _index)
    {
        return item_total_arr_[_index];
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
        foreach (var item_data in item_total_arr_)
        {
            // item_type_dic
            if (!item_type_dic_.ContainsKey(item_data.item_type))
            {
                item_type_dic_[item_data.item_type] = new List<ItemData>();
            }
            item_type_dic_[item_data.item_type].Add(item_data);

            // item_total_rarlity_dic
            if (!item_total_rarlity_dic_.ContainsKey(item_data.item_rarlity))
            {
                item_total_rarlity_dic_[item_data.item_rarlity] = new List<ItemData>();
            }
            item_total_rarlity_dic_[item_data.item_rarlity].Add(item_data);

            // item_type_rarlity_dic_
            if (!item_type_rarlity_dic_.ContainsKey(item_data.item_type))
            {
                item_type_rarlity_dic_[item_data.item_type] = new Dictionary<int, List<ItemData>>();
            }
            if (!item_type_rarlity_dic_[item_data.item_type].ContainsKey(item_data.item_rarlity))
            {
                item_type_rarlity_dic_[item_data.item_type][item_data.item_rarlity] = new List<ItemData>();
            }
            item_type_rarlity_dic_[item_data.item_type][item_data.item_rarlity].Add(item_data);
        }
    }

    public ItemData getRandomItemInTotalItem(RarlityPercentPair[] rarlity_arr)
    {
        int select_rarlity = Utility.getRandRarlityByRarlityArr(rarlity_arr);
        var select_item_list = item_total_rarlity_dic_[select_rarlity];
        int select_index = Random.Range(0, select_item_list.Count);
        return select_item_list[select_index];
    }

    public ItemData getRandomItemInTypeItem(RarlityPercentPair[] rarlity_arr, ItemType item_type)
    {
        int select_rarlity = Utility.getRandRarlityByRarlityArr(rarlity_arr);
        var select_item_list = item_type_rarlity_dic_[item_type][select_rarlity];
        int select_index = Random.Range(0, select_item_list.Count);
        return select_item_list[select_index];
    }
}

[System.Serializable]
public struct StatValuePair
{
    public StatType stat_type;
    public int value;
}

