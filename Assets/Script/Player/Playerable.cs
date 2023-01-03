using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerable : MonoBehaviour
{
    private PlayerableData playerable_data_;
    public PlayerableData playerable_data { get => playerable_data_; }

    public void initPlayerable(int _index_no)
    {
        playerable_data_ = PlayerableDatabase.instance.getPlayerableDataToIndex(_index_no);
    }
}
