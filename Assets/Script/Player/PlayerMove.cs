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
            TurnSystem.instance.runTurn(1f);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movePlayer(1);
            TurnSystem.instance.runTurn(1f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movePlayer(2);
            TurnSystem.instance.runTurn(1f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movePlayer(3);
            TurnSystem.instance.runTurn(1f);
        }
    }

    public void init()
    {
        curr_pos_ = MapManager.instance.getCurMap().getRandomRoomTilePos();
        move(curr_pos_);
    }

    public void move(Vector2Int pos)
    {
        updateSurroundTileView(false);
        curr_pos_ = pos;
        transform.position = MapManager.instance.getCurMap().getRealPosByTilePos(curr_pos_);
        MapManager.instance.getCurMap().getTileByTilePos(curr_pos_).setChildEntity(transform);
        UIManager.instance.pickup_ui.updatePickUpByTilePos(curr_pos);
        updateSurroundTileView(true);
    }
    
    public void movePlayer(int dir)
    {
        var pos = curr_pos_ + Utility.int_to_vector_int[dir];
        if (MapManager.instance.getCurMap().isOnMap(pos))
        {
            if (MapManager.instance.getCurMap().getTileByTilePos(pos).walkable)
            {
                move(pos);
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
                if (MapManager.instance.getCurMap().isOnMap(pos))
                {
                    var tile = MapManager.instance.getCurMap().getTileByTilePos(pos);
                    tile.updateVisible(can_view);
                }
                   
            }
        }

        for (int y = -1; y >= -view_range ; y--)
        {
            for (int x = -view_range - y; x <= view_range + y; x++)
            {
                var pos = curr_pos_ + new Vector2Int(x, y);
                if (MapManager.instance.getCurMap().isOnMap(pos))
                {
                    var tile = MapManager.instance.getCurMap().getTileByTilePos(pos);
                    tile.updateVisible(can_view);
                }
            }
        }
    }
}