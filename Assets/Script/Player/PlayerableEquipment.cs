using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerableEquipment
{
    private int weapon_index_;
    private int armor_index_;
    private int[] ring_index_arr_ = new int[2];
    private int neckless_index_;
    
    public void equipWeapon(int _inventory_num)
    {
        weapon_index_ = _inventory_num;
    }

    public void equipArmor(int _inventory_num)
    {
        armor_index_ = _inventory_num;
    }
    
    public void equipRing(int _inventory_num, int _index)
    {
        ring_index_arr_[_index] = _inventory_num;
    }

    public void equipNeckless(int _inventory_num)
    {
        neckless_index_ = _inventory_num;
    }
}
