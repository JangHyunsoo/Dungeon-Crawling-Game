using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int tile_type_;
    private TileData tile_data_;
    private PathData path_data_ = new PathData();
    private List<Item> item_list_ = new List<Item>();

    [SerializeField]
    private Transform entity_parent_;
    [SerializeField]
    private SpriteRenderer cloud_rd_;

    public int tile_type { get => tile_type_; }
    public Vector2Int tile_pos { get => MapManager.instance.tile_map.convertWorldPos2TilePos(transform.position); }
    public TileData tile_data { get => tile_data_; }
    public PathData path_data { get => path_data_; }
    public GameObject entity_go { get => entity_parent_.GetChild(0).gameObject; }
    public bool is_under { get => entity_parent_.childCount != 0; }
    public bool walkable { get => tile_data_.walkable && !is_under; }
    public bool walkable_without_player { get => tile_data_.walkable && (!is_under || entity_go.CompareTag("Player")); }


    private Color memory_color = new Color(1f, 1f, 1f, 0.5f);

    public bool checkWalkable(GameObject _entity)
    {
        if(!is_under || checkEqualEntity(_entity))
        {
            return tile_data_.walkable;
        }
        else
        {
            return false;
        }
    }

    private bool checkEqualEntity(GameObject _entity)
    {
        if (!is_under)
        {
            return false;
        }
        else
        {
            return entity_go == _entity;
        }
    }

    public void setType(int _tile_type)
    {
        tile_type_ = _tile_type;
        tile_data_ = TileDataBase.instance.getTileData(_tile_type);
        GetComponent<SpriteRenderer>().sprite = tile_data_.tile_sprite;
        cloud_rd_.color = Color.white;
    }

    public void setChildEntity(Transform _entity_go)
    {
        _entity_go.SetParent(entity_parent_);
    }

    public void updateVisible(bool can_view)
    {
        if (tile_data_.is_wall)
        {
            var my_pos = MapManager.instance.tile_map.convertWorldPos2TilePos(transform.position);
            cloud_rd_.color = Color.white;

            foreach (var item in MapManager.instance.tile_map.getNeighbourTile(my_pos))
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