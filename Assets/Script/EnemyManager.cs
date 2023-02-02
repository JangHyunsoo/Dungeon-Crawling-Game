using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField]
    private GameObject enemy_prefab_;
    private List<Enemy> enemy_list_ = new List<Enemy>();

    public void startStage()
    {
        for (int i = 0; i < 20; i++)
        {
            // var enemy_data = EnemyDatabase.instance.getRandomEnemyInTotalEnemy(StageManager.instance.curr_stage_data.mosnter_rand_amount_arr);
            // createEnemyInRandomTile(enemy_data);
        }
    }

    public Enemy createEnemyInRandomTile(EnemyData enemy_data)
    {
        TileMap tile_map = MapManager.instance.getCurMap();
        var enemy_go = GameObject.Instantiate(enemy_prefab_);
        var enemy_cp = enemy_go.GetComponent<Enemy>();
        enemy_cp.enemy_ai.move(tile_map.getRandomRoomTilePos());
        enemy_cp.initEnemyData(enemy_data.index_no);
        enemy_list_.Add(enemy_cp);
        return enemy_cp;
    }
}
