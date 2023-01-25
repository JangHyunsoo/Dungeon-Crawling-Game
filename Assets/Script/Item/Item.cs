using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected ItemData item_data_;
    public ItemData item_data { get => item_data_; }

    private int amount_;
    public int amount { get => amount_; }

    public Item(ItemData _item_data, int _amount = 1)
    {
        item_data_ = _item_data;
        amount_ = _amount;
    }
}
