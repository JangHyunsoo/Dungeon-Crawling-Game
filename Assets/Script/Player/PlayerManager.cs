using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    private Playable playable_;
    public Playable playable { get => playable_; }

    private PlayableApSystem playable_ap_system_;
    public PlayableApSystem playable_ap_system { get => playable_ap_system; }

    public void Start()
    {
        playable_.init(0);
    }
}
