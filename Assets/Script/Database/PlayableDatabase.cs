using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableDatabase : Singleton<PlayableDatabase>
{
    [SerializeField]
    private PlayableData[] playable_data_arr_;
    
    public PlayableData getPlayableDataToIndex(int _index_no)
    {
        return playable_data_arr_[_index_no];
    }
}
