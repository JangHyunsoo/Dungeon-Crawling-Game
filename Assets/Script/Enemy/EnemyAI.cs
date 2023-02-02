using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    protected Vector2Int curr_pos_;
    protected List<Vector2Int> path_list_;

    public virtual void act()
    {
        getPath();

        if (path_list_.Count == 0)
        {

        }
        else
        {
            if (MapManager.instance.tile_map.getTileByTilePos(path_list_[0]).walkable)
            {
                move(path_list_[0]);
            }
        }
    }

    public void move(Vector2Int pos)
    {
        curr_pos_ = pos;
        transform.position = MapManager.instance.tile_map.getRealPosByTilePos(curr_pos_);
        MapManager.instance.tile_map.getTileByTilePos(curr_pos_).setChildEntity(transform);
    }

    protected void getPath()
    {
        var target_pos_ = PlayerManager.instance.player_move.curr_pos;
        path_list_ = MapManager.instance.tile_map.findPath(curr_pos_, target_pos_, 100);
    }
}
