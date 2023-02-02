using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipUI : MonoBehaviour
{
    [SerializeField]
    private EquipSlotUI[] equip_slot_arr_;
    
    public void updateSlots()
    {
        foreach (var slot in equip_slot_arr_)
        {
            slot.updateEquipSlot();
        }
    }
}
