using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDataBase : Singleton<TileDataBase>
{
    [SerializeField]
    private GameObject tile_prefab_;
    [SerializeField]
    private TileData[] tile_data_arr_;

    public void init()
    {
        System.Array.Sort(tile_data_arr_, delegate(TileData one, TileData other)
        {
            if (one.tile_no > other.tile_no) return 1;
            else if (one.tile_no == other.tile_no) return 0;
            else return -1;
        });
    }

    public TileData getTileData(int _idx)
    {
        return tile_data_arr_[_idx];
    }

    public GameObject createTile(Vector2 _pos, int _idx)
    {
        var tile = GameObject.Instantiate(tile_prefab_, _pos, Quaternion.identity);
        tile.GetComponent<Tile>().setType(_idx);
        return tile;
    }

    public GameObject createTile(float x, float y, int _idx)
    {
        return createTile(new Vector2(x, y), _idx);
    }
}
