using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField]
    private MapGeneration map_generation_;
    [SerializeField]
    private TileMap tile_map_;

    public TileMap tile_map { get => tile_map_; }

    public IEnumerator init()
    {
        yield return StartCoroutine(map_generation_.gernationMap());
    }
}
