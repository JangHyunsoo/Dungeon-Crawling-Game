using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    protected Vector2Int tile_pos_;
    private bool is_walkable_;

    public void setTilePos(Vector2Int _tile_pos)
    {
        tile_pos_ = _tile_pos;
    }

    public virtual void unberAct()
    {

    }

    public virtual void useAct()
    {

    }

    public virtual void exitAct()
    {

    }
}