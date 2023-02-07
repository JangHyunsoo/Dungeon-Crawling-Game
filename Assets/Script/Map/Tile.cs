using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int tile_type_;
    private TileData tile_data_;
    private Vector2Int tile_pos_;
    private PathData path_data_ = new PathData();

    [SerializeField]
    private Transform entity_parent_;
    [SerializeField]
    private Transform item_parent_;
    [SerializeField]
    private Transform object_parent_;

    [SerializeField]
    private SpriteRenderer cloud_rd_;

    public int tile_type { get => tile_type_; }
    public Vector2Int tile_pos { get => tile_pos_; }
    public TileData tile_data { get => tile_data_; }
    public PathData path_data { get => path_data_; }
    public GameObject entity_go { get => entity_parent_.GetChild(0).gameObject; }
    public bool is_under_item { get => item_parent_.childCount != 0; }
    public bool is_under_object { get => object_parent_.childCount != 0; }
    public bool is_under_entity { get => entity_parent_.childCount != 0; }
    public bool walkable { get => tile_data_.walkable && !is_under_entity; }
    public bool walkable_without_player { get => tile_data_.walkable && (!is_under_entity || entity_go.CompareTag("Player")); }

    private Color memory_color = new Color(1f, 1f, 1f, 0.5f);

    public void setTilePos(Vector2Int pos)
    {
        tile_pos_ = pos;
    }

    public Item getDropItem(int _idx)
    {
        if(item_parent_.childCount <= _idx)
        {
            Debug.Log(name + " : nothing here.");
            return null;
        }
        else
        {
            return item_parent_.GetChild(_idx).GetComponent<DropItem>().item;
        }
    }

    public DropItem[] getDropItems()
    {
        return item_parent_.GetComponentsInChildren<DropItem>();
    }

    public void addDropItem(DropItem _drop_item)
    {
        _drop_item.transform.SetParent(item_parent_);
        _drop_item.transform.position = item_parent_.position;
    }

    public void setType(int _tile_type)
    {
        tile_type_ = _tile_type;
        tile_data_ = TileDataBase.instance.getTileData(_tile_type);
        GetComponent<SpriteRenderer>().sprite = tile_data_.tile_sprite;
        cloud_rd_.color = Color.white;
    }

    public void addObject(TileObject _tile_object)
    {
        _tile_object.transform.SetParent(object_parent_);
        _tile_object.transform.position = object_parent_.position;
        _tile_object.setTilePos(tile_pos_);
    }

    public TileObject getTileObject(int _idx)
    {
        return object_parent_.GetChild(_idx).GetComponent<TileObject>();
    }

    public void addEntity(Transform _entity_go)
    {
        _entity_go.SetParent(entity_parent_);
    }

    public void updateVisible(bool can_view)
    {
        if (tile_data_.is_wall)
        {
            var my_pos = MapManager.instance.getCurMap().convertWorldPos2TilePos(transform.position);
            cloud_rd_.color = Color.white;

            foreach (var item in MapManager.instance.getCurMap().getNeighbourTile(my_pos))
            {
                if (!item.tile_data_.is_wall)
                {
                    if (can_view)
                    {
                        cloud_rd_.color = Color.clear;
                    }
                    else
                    {
                        cloud_rd_.color = memory_color;
                    }
                    break;
                }
            }
        }
        else
        {
            if (can_view)
            {
                cloud_rd_.color = Color.clear;
            }
            else
            {
                cloud_rd_.color = memory_color;
            }
        }
    }
}

public class PathData
{
    public int gCost = 0;
    public int hCost = 0;
    public Tile parent;

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}