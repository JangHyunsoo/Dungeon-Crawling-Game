using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool start_game_ = false;
    public bool start_game { get => start_game_; }
    
    void Start()
    {
        initDataBase();
        initManager();
    }

    private void initDataBase()
    {
        TileDataBase.instance.init();
        ItemDatabase.instance.init();
    }

    private void initManager()
    {
        StartCoroutine(initManagerCoroutine());
    }

    private IEnumerator initManagerCoroutine()
    {
        yield return StartCoroutine(MapManager.instance.init());
        PlayerManager.instance.init();
        EnemyManager.instance.startStage();
        ItemManager.instance.init();
        start_game_ = true;
    }
}
