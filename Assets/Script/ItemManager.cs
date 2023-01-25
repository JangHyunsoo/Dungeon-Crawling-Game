using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField]
    private GameObject drop_item_prefab_;
    
    public void init()
    {
        for (int i = 0; i < 10; i++)
        {
            var tile_pos = MapManager.instance.tile_map.getRandomRoomTilePos();
            var tile = MapManager.instance.tile_map.getTileByTilePos(tile_pos);
            tile.addDropItem(createDropItem(0));
        }
    }

    public DropItem createDropItem(int _idx)
    {
        var drop_item_go = GameObject.Instantiate(drop_item_prefab_);
        var drop_item_cp = drop_item_go.GetComponent<DropItem>();
        var item = createItem(_idx);
        drop_item_cp.setItem(item);
        return drop_item_cp;
    }

    private Item createItem(int _idx)
    {
        ItemData item_data = ItemDatabase.instance.getItemData(_idx);

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
            default:
                Debug.LogError("null item type");
                return new Item(item_data);
        }

    }
}