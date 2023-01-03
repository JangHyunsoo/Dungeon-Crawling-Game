using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private ItemSlotUI[] item_slot_ui_arr_;

    private int cur_seleted_slot_num_;
}
