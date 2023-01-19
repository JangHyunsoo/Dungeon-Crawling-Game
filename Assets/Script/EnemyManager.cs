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
            createEnemy();
        }
    }

    public Enemy createEnemy()
    {
        TileMap tile_map = MapManager.instance.tile_map;
        var enemy_go = GameObject.Instantiate(enemy_prefab_);
        var enemy_cp = enemy_go.GetComponent<Enemy>();
        enemy_cp.enemy_ai.move(tile_map.getRandomRoomTilePos());
        enemy_cp.initEnemyData(0);
        enemy_list_.Add(enemy_cp);
        return enemy_cp;
    }
}
