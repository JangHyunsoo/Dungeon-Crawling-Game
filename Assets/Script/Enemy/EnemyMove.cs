using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector2Int curr_pos_;

    private float speed_;
    private float curr_ap_;
    private Vector2Int target_pos_;

    public void Start()
    {
        InvokeRepeating("act", 1f, 1f);
    }

    public void act()
    {
        moveAi();
    }

    public void move(Vector2Int pos)
    {
        curr_pos_ = pos;
        transform.position = MapManager.instance.tile_map.getRealPosByTilePos(curr_pos_);
    }

    private void moveAi()
    {
        target_pos_ = PlayerMove.instance.curr_pos;
        var path = MapManager.instance.tile_map.findPath(curr_pos_, target_pos_);   
        
        if(path.Count != 0)
        {
            move(path[0]);
        }
    }
}
