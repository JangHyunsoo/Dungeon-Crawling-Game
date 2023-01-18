using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerable
{
    private PlayerableData playerable_data_;
    public PlayerableData playerable_data { get => playerable_data_; }

    // entity 관리 어떻게 해야할지... 고민해봐야할듯
    private int cur_hp_;
    public int cur_hp { get => cur_hp; }

    public void init(int _index_no)
    {
        playerable_data_ = PlayerableDatabase.instance.getPlayerableDataToIndex(_index_no);
    }
}
