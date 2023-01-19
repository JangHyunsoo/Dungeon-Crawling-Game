using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EntityMove
{
    private float speed_;
    private float curr_ap_;
    private Vector2Int target_pos_;

    private float delay_ = 0f;

    public void Start()
    {

    }

    public void Update()
    {
        delay_ += Time.deltaTime;

        if(delay_ >= 1f)
        {
            act();
            delay_ -= 1f;
        }
    }

    public void act()
    {
        moveAi();
    }

    private void moveAi()
    {
        target_pos_ = PlayerMove.instance.curr_pos;
        var path = MapManager.instance.tile_map.findPath(curr_pos_, target_pos_, 100);   
        
        if(path.Count != 0)
        {
            if (MapManager.instance.tile_map.getTileByTilePos(path[0]).walkable)
            {
                move(path[0]);
            }
        }
    }
}
