using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
    protected Vector2Int curr_pos_;

    public void move(Vector2Int pos)
    {
        curr_pos_ = pos;
        transform.position = MapManager.instance.tile_map.getRealPosByTilePos(curr_pos_);
        MapManager.instance.tile_map.getTileByTilePos(curr_pos_).setChildEntity(transform);
    }
}
