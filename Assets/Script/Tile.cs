using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int tile_type_;
    private bool is_empty_ = true;

    public int tile_type { get => tile_type_; }
    public bool is_empty { get => is_empty_; }

    private bool invisible = false;

    public void setType(int _tile_type)
    {
        tile_type_ = _tile_type;
        GetComponent<SpriteRenderer>().sprite = TileDataBase.instance.getSprite(_tile_type);
    }
}
