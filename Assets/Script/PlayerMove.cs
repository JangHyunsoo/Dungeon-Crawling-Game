using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    private Vector2Int curr_pos_; // tile pos

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
        if (MapManager.instance.tile_map.getTileByTilePos(pos).tile_data.can_under)
        {
            setPositionByTilePos(pos);
        }
        else
        {
            Debug.Log("cannot move");
        }
    }

    private void updateSurroundTileView(bool can_view)
    {
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                var tile = MapManager.instance.tile_map.getTileByTilePos(curr_pos_ + new Vector2Int(x,y));
                tile.updateVisible(can_view);
            }
        }
    }
}