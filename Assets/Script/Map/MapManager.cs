using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField]
    private MapGeneration map_generation_;
    [SerializeField]
    private List<TileMap> tile_map_list_ = new List<TileMap>();
    public List<TileMap> tile_map_list { get => tile_map_list_; }

    private int cur_stage_ = 0;

    public IEnumerator generateMap(StageData _stage_data)
    {
        yield return StartCoroutine(map_generation_.generate(_stage_data));
        tile_map_list_.Add(map_generation_.tile_map);
    }

    public TileMap getCurMap()
    {
        return tile_map_list_[cur_stage_];
    }
}
