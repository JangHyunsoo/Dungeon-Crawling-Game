using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : TileObject
{
    private int my_floor_;
    private int my_idx_;
    private int target_floor_;
    private int target_idx_;

    public int my_floor { get => my_floor_; }
    public int my_idx { get => my_idx_; }
    public int target_floor { get => target_floor_; }
    public int target_idx { get => target_idx_; }


    public void setMyInfo(int _my_floor, int _my_idx)
    {
        my_floor_ = _my_floor;
        my_idx_ = _my_idx;
    }

    public void setTargetInfo(int _target_floor, int _target_idx)
    {
        target_floor_ = _target_floor;
        target_idx_ = _target_idx;
    }

    public override void useAct()
    {
        var target = MapManager.instance.getTileMap(target_floor_).getStair(target_idx_);
        MapManager.instance.changeStage(target_floor_);
        PlayerManager.instance.player_move.move(target.tile_pos_);
    }
}
