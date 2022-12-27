using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private float tile_size_ = 0.32f;
    private int world_width_;
    private int world_height_;
    private Vector2Int world_tile_min_pos_;
    private Tile[,] world_tile_type_arr_; // x, y

    public void setWorldPos(int _world_width, int _world_height, Vector2Int _world_tile_min_pos)
    {
        world_width_ = _world_width;
        world_height_ = _world_height;
        world_tile_min_pos_ = _world_tile_min_pos;
        world_tile_type_arr_ = new Tile[world_width_, world_height_];

        for (int i = 0; i < world_width_; i++)
        {
            for (int j = 0; j < world_height_; j++)
            {
                var tile = TileDataBase.instance.createTile(convertArrayPos2RealPos(i, j), 0);
                world_tile_type_arr_[i, j] = tile.GetComponent<Tile>();
            }
        }
    }

    public void setTileByArrayPos(Vector2Int pos, int _tile_type)
    {
        var temp = convertTilePos2ArrayPos(pos);
        world_tile_type_arr_[temp.x, temp.y].setType(_tile_type);
    }

    public void setTileByArrayPos(int x, int y, int _tile_type)
    {
        setTileByArrayPos(new Vector2Int(x, y), _tile_type);
    }

    public void setTileByWorldPos(Vector2 pos, int _tile_type)
    {
        var temp = convertWorldPos2ArrayPos(pos);
        try
        {
            world_tile_type_arr_[temp.x, temp.y].setType(_tile_type);
        }
        catch
        {
            Debug.LogWarning(temp);
            Debug.LogWarning(world_width_);
            Debug.LogWarning(world_height_);
        }
    }

    private Vector2 convertArrayPos2RealPos(Vector2Int pos)
    {
        return new Vector2((pos.x + world_tile_min_pos_.x) * 0.32f, (pos.y + world_tile_min_pos_.y) * 0.32f);
    }

    private Vector2 convertArrayPos2RealPos(int x, int y)
    {
        return new Vector2((x + world_tile_min_pos_.x) * 0.32f, (y + world_tile_min_pos_.y) * 0.32f);
    }

    private Vector2Int convertTilePos2ArrayPos(int x, int y)
    {
        return new Vector2Int(x, y) - world_tile_min_pos_;
    }

    private Vector2Int convertTilePos2ArrayPos(Vector2Int pos)
    {
        return pos - world_tile_min_pos_;
    }

    public Vector2Int convertWorldPos2IndexPos(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt(pos.x / tile_size_ - 0.16f), Mathf.RoundToInt(pos.y / tile_size_ - 0.16f));
    }

    public Vector2Int convertWorldPos2ArrayPos(Vector2 pos)
    {
        return convertTilePos2ArrayPos(convertWorldPos2IndexPos(pos));
    }
}