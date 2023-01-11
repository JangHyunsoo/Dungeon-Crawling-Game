using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerableEquipment
{
    private Weapon weapon_ = null;
    public Weapon weapon { get => weapon_; }
    private Armor armor_;
    public Armor armor { get => armor_; }
    private Item[] ring_arr_ = new Item[2];
    
    public void equipWeapon(Item _equiped_weapon)
    {
        if(weapon_ != null)
        {
            foreach (var addition_type in weapon_.addition_stat_list)
            {
                PlayerManager.instance.playerable_stat.addValue(addition_type.stat_type, -addition_type.value);
            }
        }

        weapon_ = (Weapon)_equiped_weapon;

        foreach (var addition_type in weapon_.addition_stat_list)
        {
            PlayerManager.instance.playerable_stat.addValue(addition_type.stat_type, addition_type.value);
        }
    }

    public void equipArmor(Item _equiped_armor)
    {
        if (armor_ != null)
        {
            foreach (var addition_type in armor_.addition_stat_list)
            {
                PlayerManager.instance.playerable_stat.addValue(addition_type.stat_type, -addition_type.value);
            }
        }

        armor_ = (Armor)_equiped_armor;

        foreach (var addition_type in armor_.addition_stat_list)
        {
            PlayerManager.instance.playerable_stat.addValue(addition_type.stat_type, addition_type.value);
        }

    }


    public Weapon getWeapon()
    {
        return weapon_;
    }
    
}
