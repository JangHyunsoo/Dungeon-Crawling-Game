using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected ItemData item_data_;
    public ItemData item_data { get => item_data_; }

    private int amount_;
    public int amount;
}
