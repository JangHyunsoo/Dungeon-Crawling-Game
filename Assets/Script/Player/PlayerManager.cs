using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private Playable playable_;
    public Playable playable { get => playable_; }

    public void Start()
    {
        playable_.init(0);
    }
}
