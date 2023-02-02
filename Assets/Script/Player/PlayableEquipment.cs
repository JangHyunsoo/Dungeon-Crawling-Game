using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableEquipment : MonoBehaviour
{
    private Weapon none_weapon_;
    private Armor none_armor_;
    private Ring none_ring_;

    [SerializeField]
    private ItemData[] none_item_data_arr_;

    private Weapon weapon_;
    public Weapon weapon { get => weapon_; }

    private Armor armor_;
    public Armor armor { get => armor_; }

    private Ring[] ring_arr_ = new Ring[2];
    public Ring[] ring_arr { get => ring_arr_; }

    public void init()
    {
        none_weapon_ = new Weapon(none_item_data_arr_[0]);
        none_armor_ = new Armor(none_item_data_arr_[1]);
        none_ring_ = new Ring(none_item_data_arr_[2]);
        
        // init playable primary item setting 
    }


    public Item getEquipItem(ItemType _item_type, int _idx = 0)
    {
        switch (_item_type)
        {
            case ItemType.WEAPON:
                return weapon_;
            case ItemType.ARMOR:
                return armor_;
            case ItemType.RING:
                return ring_arr_[_idx];
            default:
                return null;
        }
    }

    public void unequipItem(ItemType _item_type, int _idx = 0)
    {
        switch (_item_type)
        {
            case ItemType.WEAPON:
                unequipWeapon();
                break;
            case ItemType.ARMOR:
                unequipArmor();
                break;
            case ItemType.RING:
                unequipRing(_idx);
                break;
            default:
                break;
        }
    }

    public void equipItem(Item _item)
    {
        switch (_item.item_data.item_type)
        {
            case ItemType.WEAPON:
                weapon_ = (Weapon)_item;
                break;
            case ItemType.ARMOR:
                armor_ = (Armor)_item;
                break;
            case ItemType.RING:
                if (ring_arr_[0] != none_ring_)
                {
                    ring_arr_[0] = (Ring)_item;
                }
                else if(ring_arr_[1] != none_ring_)
                {   
                    ring_arr_[1] = (Ring)_item;
                }
                else
                {
                    ring_arr_[0] = (Ring)_item;
                }
                break;
            default:
                break;
        }
    }

    public void equipWeapon(Weapon _weapon)
    {
        weapon_ = _weapon;
    }

    public void unequipWeapon()
    {
        weapon_ = none_weapon_;
    }

    public void equipArmor(Armor _armor)
    {
        armor_ = _armor;
    }

    public void unequipArmor()
    {
        armor_ = none_armor_;
    }

    public void equipRing(Ring _ring, int _index)
    {
        ring_arr_[_index] = _ring;
    }

    public void unequipRing(int _index)
    {
        ring_arr_[_index] = none_ring_;
    }
}
