using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int tile_type_;
    private TileData tile_data_;
    private PathData path_data_ = new PathData();
    private EnemyMove enemy_;
    private PlayerMove player_;
    private List<Item> item_list_ = new List<Item>();
    private bool is_empty_ = true;

    public int tile_type { get => tile_type_; }
    public Vector2Int tile_pos { get => MapManager.instance.tile_map.convertWorldPos2TilePos(transform.position); }
    public TileData tile_data { get => tile_data_; }
    public PathData path_data { get => path_data_; }
    public bool is_empty { get => is_empty_; }
    public bool walkable { get => tile_data_.walkable && is_empty_; }

    [SerializeField]
    private SpriteRenderer cloud_rd_;
    [SerializeField]
    private Vector2Int my_pos_;

    private Color memory_color = new Color(1f, 1f, 1f, 0.5f);

    public void setType(int _tile_type)
    {
        tile_type_ = _tile_type;
        tile_data_ = TileDataBase.instance.getTileData(_tile_type);
        GetComponent<SpriteRenderer>().sprite = tile_data_.tile_sprite;
        cloud_rd_.color = Color.white;
    }

    public void updateVisible(bool can_view)
    {
        my_pos_ = MapManager.instance.tile_map.convertWorldPos2TilePos(transform.position);

        if (tile_data_.is_wall)
        {
            var my_pos = MapManager.instance.tile_map.convertWorldPos2TilePos(transform.position);
            cloud_rd_.color = Color.white;

            foreach (var dir in Utility.int_to_vector_int8)
            {
                if(!MapManager.instance.tile_map.isOnMap(my_pos + dir))
                {
                    continue;
                }
                else
                {
                    var other = MapManager.instance.tile_map.getTileByTilePos(my_pos + dir);
                    if (!other.tile_data_.is_wall)
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