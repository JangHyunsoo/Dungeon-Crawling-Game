using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDataBase : Singleton<TileDataBase>
{
    [SerializeField]
    private GameObject tile_prefab_;
    [SerializeField]
    private Sprite[] tile_sprite_arr_;
    [SerializeField]
    private Transform tile_holder_;

    public Sprite getSprite(int _idx)
    {
        return tile_sprite_arr_[_idx];
    }

    public GameObject createTile(Vector2 _pos, int _idx)
    {
        var tile = GameObject.Instantiate(tile_prefab_, _pos, Quaternion.identity);
        tile.GetComponent<Tile>().setType(_idx);
        //tile.transform.SetParent(this.gameObject.transform);
        return tile;
    }

    public GameObject createTile(float x, float y, int _idx)
    {
        return createTile(new Vector2(x, y), _idx);
    }
}
