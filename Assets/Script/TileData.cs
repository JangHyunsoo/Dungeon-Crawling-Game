using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    [SerializeField]
    private int tile_no_;
    public int tile_no { get => tile_no_; }

    [SerializeField]
    private Sprite tile_sprite_;
    public Sprite tile_sprite { get => tile_sprite_; }

    [SerializeField]
    private bool can_under_ = true;
    public bool can_under { get => can_under_; }
}
