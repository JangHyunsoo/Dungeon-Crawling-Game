using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playable : MonoBehaviour
{
    private PlayableData playable_data_;
    public PlayableData playable_data { get => playable_data_; }

    [SerializeField]
    private PlayableStat playable_stat_;
    public PlayableStat playable_stat { get => playable_stat_; }

    [SerializeField]
    private PlayableAptitude playable_aptitude_;
    public PlayableAptitude playable_aptitude { get => playable_aptitude_; }

    [SerializeField]
    private PlayableInventory playable_inventory_;
    public PlayableInventory playable_inventory { get => playable_inventory_; }

    [SerializeField]
    private PlayableEquipment playable_equipment_;
    public PlayableEquipment playable_equipment { get => playable_equipment_; }

    private int cur_hp_;
    public int cur_hp { get => cur_hp; }

    public void init(int _index_no)
    {
        playable_data_ = PlayableDatabase.instance.getPlayableDataToIndex(_index_no);
        playable_stat_.init();
        playable_aptitude_.init();
        playable_inventory_.init();
        playable_equipment_.init();
    }
}
