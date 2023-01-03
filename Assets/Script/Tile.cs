using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int tile_type_;
    private TileData tile_data_;
    private bool is_empty_ = true;

    public int tile_type { get => tile_type_; }
    public TileData tile_data { get => tile_data_; }
    public bool is_empty { get => is_empty_; }

    [SerializeField]
    private SpriteRenderer cloud_rd_;

    private Color memory_color = new Color(1f, 1f, 1f, 0.5f);

    public void setType(int _tile_type)
    {
        tile_type_ = _tile_type;
        tile_data_ = TileDataBase.instance.getTileData(_tile_type);
        GetComponent<SpriteRenderer>().sprite = tile_data_.tile_sprite;
        cloud_rd_.color = Color.white;
    }

    public void updateVisible(bool can_view)
    {
        if (can_view)
        {
            cloud_rd_.color = Color.clear;
        }
        else
        {
            cloud_rd_.color = memory_color;
        }
    }
}
