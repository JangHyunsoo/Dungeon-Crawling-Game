using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : EquipItem
{
    public Ring(ItemData _item_data) : base(_item_data)
    {
        var cur_ring_data = (RingData)_item_data;

        switch (cur_ring_data.ring_type)
        {
            case RingType.HPREGEN:
                addition_stat_dic[StatType.HP_REGEN] += 1f;
                break;
            case RingType.MPREGEN:
                addition_stat_dic[StatType.MP_REGEN] += 1f;
                break;
            case RingType.MAGICRES:
                Debug.Log("Magic res");
                break;
        }
    }
}
