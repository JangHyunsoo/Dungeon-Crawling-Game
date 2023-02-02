using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private ItemSlotUI[] item_slot_ui_arr_;

    public void init()
    {
        for (int i = 0; i < item_slot_ui_arr_.Length; i++)
        {
            item_slot_ui_arr_[i].setIndex(i);
        }
    }

    public void updateItemSlots()
    {
        for (int i = 0; i < item_slot_ui_arr_.Length; i++)
        {
            updateItemSlot(i);
        }
    }

    public void updateItemSlot(int _idx)
    {
        item_slot_ui_arr_[_idx].setItemSlotData();
    }
}
