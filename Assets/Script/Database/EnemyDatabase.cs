using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDatabase : Singleton<EnemyDatabase>
{
    [SerializeField]
    private EnemyData[] enemy_data_arr_;
    private Dictionary<int, List<EnemyData>> enemy_total_rarlity_dic_ = new Dictionary<int, List<EnemyData>>();

    public void init()
    {
        foreach (var enemy_data in enemy_data_arr_)
        {
            if (!enemy_total_rarlity_dic_.ContainsKey(enemy_data.rarlity))
            {
                enemy_total_rarlity_dic_[enemy_data.rarlity] = new List<EnemyData>();
            }
            enemy_total_rarlity_dic_[enemy_data.rarlity].Add(enemy_data);
        }
    }

    public EnemyData getEnemyData(int _index)
    {
        return enemy_data_arr_[_index];
    }

    public EnemyData getRandomEnemyInTotalEnemy(RarlityPercentPair[] rarlity_arr)
    {
        int select_rarlity = Utility.getRandRarlityByRarlityArr(rarlity_arr);
        var select_enemy_list = enemy_total_rarlity_dic_[select_rarlity];
        int select_index = Random.Range(0, select_enemy_list.Count);
        return select_enemy_list[select_index];
    }
}