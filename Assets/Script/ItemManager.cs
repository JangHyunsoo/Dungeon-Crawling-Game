using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    private GameObject drop_item_prefab_;
    
    public void init()
    {
        for (int i = 0; i < 100; i++)
        {
            var item_data = ItemDatabase.instance.getRandomItemInTotalItem(StageManager.instance.curr_stage_data.item_rand_amount_arr);
            createDropItemInRandomPos(item_data);
        }
    }

    public DropItem createDropItemInRandomPos(ItemData item_data)
    {
        var tile = MapManager.instance.tile_map.getRandomRoomTile();
        var drop_item = createDropItem(item_data);
        tile.addDropItem(drop_item);
        return drop_item;
    }

    public DropItem createDropItem(ItemData item_data)
    {
        var drop_item_go = GameObject.Instantiate(drop_item_prefab_);
        var drop_item_cp = drop_item_go.GetComponent<DropItem>();
        var item = createItem(item_data);
        drop_item_cp.setItem(item);
        return drop_item_cp;
    }

    private Item createItem(ItemData item_data)
    {
        switch (item_data.item_type)
        {
            case ItemType.WEAPON:
                return new Weapon(item_data);
            case ItemType.ARMOR:
                return new Armor(item_data);
            case ItemType.RING:
                return new Item(item_data);
            case ItemType.POTION:
                return new Item(item_data);
            case ItemType.SCROLL:
                return new Item(item_data);
            case ItemType.OTHER:
                return new Item(item_data);
            case ItemType.NONE:
                return new Item(item_data);
            default:
                Debug.LogError("null item type");
                return new Item(item_data);
        }
    }
}