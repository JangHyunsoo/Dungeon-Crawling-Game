using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private float tile_size_ = 0.32f;
    private int world_width_;
    private int world_height_;
    private Room[] room_arr_;
    private Vector2Int world_tile_min_pos_;
    private Tile[,] world_tile_type_arr_;

    public void setRoomArr(List<Room> _room_list)
    {
        room_arr_ = _room_list.ToArray();
    }

    public Room getRoom(int _idx)
    {
        return room_arr_[_idx];
    }

    public int getRoomLength()
    {
        return room_arr_.Length;
    }

    public bool isOnMap(Vector2Int pos)
    {
        return pos.x >= world_tile_min_pos_.x && pos.x < world_width_ + world_tile_min_pos_.x && pos.y >= world_tile_min_pos_.y && pos.y < world_height_ + world_tile_min_pos_.y;
    }

    public Tile getTileByTilePos(Vector2Int pos)
    {
        var temp = convertTilePos2ArrayPos(pos);
        return world_tile_type_arr_[temp.x, temp.y];
    }

    public Vector2 getRealPosByTilePos(Vector2Int pos)
    {
        return getTileByTilePos(pos).transform.position;
    }

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
                tile.name = (new Vector2Int(i, j) + world_tile_min_pos_).ToString();
                tile.transform.SetParent(transform);
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
        world_tile_type_arr_[temp.x, temp.y].setType(_tile_type);
    }

    public Vector2 convertArrayPos2RealPos(Vector2Int pos)
    {
        return new Vector2((pos.x + world_tile_min_pos_.x) * tile_size_ + tile_size_ / 2, (pos.y + world_tile_min_pos_.y) * tile_size_ + tile_size_ / 2);
    }

    public Vector2 convertArrayPos2RealPos(int x, int y)
    {
        return new Vector2((x + world_tile_min_pos_.x) * tile_size_ + tile_size_ / 2, (y + world_tile_min_pos_.y) * tile_size_ + tile_size_ / 2);
    }

    public Vector2Int convertTilePos2ArrayPos(int x, int y)
    {
        return new Vector2Int(x, y) - world_tile_min_pos_;
    }

    public Vector2Int convertTilePos2ArrayPos(Vector2Int pos)
    {
        return pos - world_tile_min_pos_;
    }

    public Vector2Int convertWorldPos2TilePos(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt((pos.x - tile_size_ / 2) / tile_size_), Mathf.RoundToInt((pos.y - tile_size_ / 2) / tile_size_));
    }

    public Vector2Int convertWorldPos2ArrayPos(Vector2 pos)
    {
        return convertTilePos2ArrayPos(convertWorldPos2TilePos(pos));
    }

    public Vector2Int getIncludeTilePos(Vector2 pos)
    {
        return new Vector2Int(Mathf.RoundToInt((pos.x - tile_size_ / 2) / tile_size_), Mathf.RoundToInt((pos.y - tile_size_ / 2) / tile_size_));
    }

    public Vector2Int getRandomRoomTilePos()
    {
        int rand_room_idx = Random.RandomRange(0, room_arr_.Length);
        Room rand_room = room_arr_[rand_room_idx];
        Vector2 rand_room_pos = rand_room.getRandomRealPos();
        Vector2Int include_tile_pos = getIncludeTilePos(rand_room_pos);
        return include_tile_pos;
    }
}