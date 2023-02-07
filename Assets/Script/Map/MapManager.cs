using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField]
    private MapGeneration map_generation_;
    private List<TileMap> tile_map_list_ = new List<TileMap>();
    public List<TileMap> tile_map_list { get => tile_map_list_; }

    private int cur_stage_ = 0;

    public IEnumerator init()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return StartCoroutine(generateMap(i, StageManager.instance.curr_stage_data));
        }



        for (int i = 0; i < 5; i++)
        {
            tile_map_list[i].gameObject.active = i == 0;
        }
    }

    public IEnumerator generateMap(int _stage_no, StageData _stage_data)
    {
        yield return StartCoroutine(map_generation_.generate(_stage_no, _stage_data));
        tile_map_list_.Add(map_generation_.tile_map);
    }

    public TileMap getCurMap()
    {
        return tile_map_list_[cur_stage_];
    }

    public TileMap getTileMap(int floor)
    {
        if(floor < tile_map_list.Count)
        {
            return tile_map_list_[floor];
        }
        return null;
    }

    public void changeStage(int stage_num)
    {
        tile_map_list_[cur_stage_].gameObject.active = false;
        tile_map_list_[stage_num].gameObject.active = true;
        cur_stage_ = stage_num;
    }
}
