using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [Header("Room Info")]
    [SerializeField]
    private int room_count_;
    public int room_count { get => room_count_; }

    [SerializeField]
    private int room_max_width_;
    public int room_max_width { get => room_max_width_; }

    [SerializeField]
    private int room_max_height_;
    public int room_max_height { get => room_max_height_; }

    [SerializeField]
    private int room_min_width_;
    public int room_min_width { get => room_min_width_; }

    [SerializeField]
    private int room_min_height_;
    public int room_min_height { get => room_min_height_; }

    [Header("Monster Info")]
    [SerializeField]
    private int monster_count_;
    public int monster_count { get => monster_count_; }

    [SerializeField]
    private RarlityPercentPair[] mosnter_rand_amount_arr_;
    public RarlityPercentPair[] mosnter_rand_amount_arr { get => mosnter_rand_amount_arr_; }

    [Header("Item Info")]
    [SerializeField]
    private int item_count_;
    public int item_count { get => item_count_; }

    [SerializeField]
    private RarlityPercentPair[] item_rand_amount_arr_;
    public RarlityPercentPair[] item_rand_amount_arr { get => item_rand_amount_arr_; }

}