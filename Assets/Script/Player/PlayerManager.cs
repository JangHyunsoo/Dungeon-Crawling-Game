using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private Playable playable_;
    public Playable playable { get => playable_; }

    [SerializeField]
    private PlayerMove player_move_;
    public PlayerMove player_move { get => player_move_; }

    public void init()
    {
        playable_.init(0);
        player_move_.init();
    }
}
