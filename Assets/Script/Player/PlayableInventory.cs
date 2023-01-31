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
        addItem(new Weapon(ItemDatabase.instance.getItemData(0)));
        UIManager.instance.inventory_ui.updateItemSlots();
    }

    public Item getItemToIndexNum(int _index)
    {
        return item_arr_[_index];
    }

    public void addItem(Item item)
    {
        for (int i = 0; i < INVENTORY_SIZE_; i++)
        {
            if(item_arr_[i] == null)
            {
                item_arr_[i] = item;
                UIManager.instance.inventory_ui.updateItemSlot(i);
                return;
            }
        }

        Debug.Log("inventory is full");
    }
}
