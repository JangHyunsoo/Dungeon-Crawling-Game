using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField]
    private GameObject enemy_prefab_;
    private List<EnemyMove> enemy_list_ = new List<EnemyMove>();


    public void startStage()
    {
        for (int i = 0; i < 15; i++)
        {
            createEnemy();
        }
    }

    public EnemyMove createEnemy()
    {
        TileMap tile_map = MapManager.instance.tile_map;
        var enemy_go = GameObject.Instantiate(enemy_prefab_);
        var enemy_cp = enemy_go.GetComponent<EnemyMove>();
        enemy_cp.move(tile_map.getRandomRoomTilePos());
        enemy_list_.Add(enemy_cp);
        return enemy_cp;
    }
}
