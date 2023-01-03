using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerableInventory
{
    private Item[] item_arr_;
    private const int INVENTORY_SIZE_ = 52;

    private PlayerableEquipment playerable_equipment_;
    public PlayerableEquipment playerable_equipment { get => playerable_equipment_; }

    public void init()
    {
        item_arr_ = new Item[INVENTORY_SIZE_];
        playerable_equipment_ = new PlayerableEquipment();

        // playerable �������� ���� �ʱ� ������ �ʱ�ȭ
    }

    public Item getItemToIndexNum(int _index)
    {
        return item_arr_[_index];
    }
}
