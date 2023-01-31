using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpUI : MonoBehaviour
{
    [SerializeField]
    private PickUpSlot[] pickup_slot_arr_;

    public void updatePickUpByTilePos(Vector2Int tile_pos)
    {
        Tile tile = MapManager.instance.tile_map.getTileByTilePos(tile_pos);
        DropItem[] dropItems = tile.getDropItems();

        for (int i = 0; i < pickup_slot_arr_.Length; i++)
        {
            if(i < dropItems.Length)
            {
                pickup_slot_arr_[i].setSlot(dropItems[i]);
                pickup_slot_arr_[i].gameObject.active = true;
            }
            else
            {
                pickup_slot_arr_[i].gameObject.active = false;
            }
        }
    }
}
