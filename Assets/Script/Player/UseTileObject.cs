using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseTileObject : MonoBehaviour
{
    public void useTileObject()
    {
        var curr_pos = PlayerManager.instance.player_move.curr_pos;
        var tile_map = MapManager.instance.getCurMap();
        var tile = tile_map.getTileByTilePos(curr_pos);
        if (tile.is_under_object)
        {
            tile.getTileObject(0).useAct();
        }
    }
}