using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playable data", menuName = "EntityData/PlayableData", order = 1)]
public class PlayableData : EntityData
{
    [SerializeField]
    private int base_strength_;
    public int base_strength { get => base_strength_; }

    [SerializeField]
    private int base_intelligent_;
    public int base_intelligent { get => base_intelligent_; }

    [SerializeField]
    private int base_dexterity_;
    public int base_dexterity { get => base_dexterity_; }

    [SerializeField]
    private int base_armor_value_;
    public int base_armor_value { get => base_armor_value_; }

    [SerializeField]
    private AptitudePair[] base_aptitude_value_arr_;
    public AptitudePair[] base_aptitude_value_arr { get => base_aptitude_value_arr_; }
}

[System.Serializable]
public struct AptitudePair
{
    public AptitudeType aptitude_type;
    public int value;
}
