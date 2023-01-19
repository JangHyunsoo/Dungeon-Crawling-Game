using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDatabase : Singleton<EnemyDatabase>
{
    [SerializeField]
    private EnemyData[] enemy_data_arr_;

    public EnemyData getEnemyData(int _index)
    {
        return enemy_data_arr_[_index];
    }
}