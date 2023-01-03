using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Playerable playerable_;
    public Playerable playerable;

    private PlayerableStat playerable_stat_;
    public PlayerableStat playerable_stat { get => playerable_stat_; }

    private PlayerableInventory playerable_inventory_;
    public PlayerableInventory playerable_inventory { get => playerable_inventory_; }


    public void init()
    {
        playerable_ = new Playerable();
        playerable_stat_ = new PlayerableStat();
        playerable_inventory_ = new PlayerableInventory();
    }
}
