using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableInventory : MonoBehaviour
{
    private Item[] item_arr_;
    private const int INVENTORY_SIZE_ = 52;

    public void init()
    {
        item_arr_ = new Item[INVENTORY_SIZE_];
    }

    public Item getItemToIndexNum(int _index)
    {
        return item_arr_[_index];
    }
}
