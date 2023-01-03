using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerableDatabase : Singleton<PlayerableDatabase>
{
    [SerializeField]
    private PlayerableData[] playerable_data_arr_;
    
    public PlayerableData getPlayerableDataToIndex(int _index_no)
    {
        return playerable_data_arr_[_index_no];
    }
}
