using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    private Vector2Int curr_pos_; // tile pos
    public Vector2Int curr_pos { get => curr_pos_; }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movePlayer(0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movePlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movePlayer(2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movePlayer(3);
        }
    }

    public void init()
    {
        curr_pos_ = MapManager.instance.tile_map.getRandomRoomTilePos();
        setPositionByTilePos(curr_pos_);
    }

    public void setPositionByTilePos(Vector2Int pos)
    {
        updateSurroundTileView(false);
        curr_pos_ = pos;
        transform.position = MapManager.instance.tile_map.getRealPosByTilePos(pos);
        updateSurroundTileView(true);
    }
    
    public void movePlayer(int dir)
    {
        var pos = curr_pos_ + Utility.int_to_vector_int[dir];
        if (MapManager.instance.tile_map.isOnMap(pos))
        {
            if (MapManager.instance.tile_map.getTileByTilePos(pos).tile_data.walkable)
            {
                setPositionByTilePos(pos);
            }
            else
            {
                Debug.Log("cannot move");
            }
        }
    }

    private void updateSurroundTileView(bool can_view)
    {
        int view_range = 4;
        for (int y = 0; y <= view_range; y++)
        {
            for (int x = -view_range + y; x <= view_range - y; x++)
            {
                var pos = curr_pos_ + new Vector2Int(x, y);
                if (MapManager.instance.tile_map.isOnMap(pos))
                {
                    var tile = MapManager.instance.tile_map.getTileByTilePos(pos);
                    tile.updateVisible(can_view);
                }
                   
            }
        }

        for (int y = -1; y >= -view_range ; y--)
        {
            for (int x = -view_range - y; x <= view_range + y; x++)
            {
                var pos = curr_pos_ + new Vector2Int(x, y);
                if (MapManager.instance.tile_map.isOnMap(pos))
                {
                    var tile = MapManager.instance.tile_map.getTileByTilePos(pos);
                    tile.updateVisible(can_view);
                }
            }
        }
    }
}