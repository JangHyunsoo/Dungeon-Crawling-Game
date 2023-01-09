using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerable data", menuName = "EntityData/PlayerableData", order = 1)]
public class PlayerableData : EntityData
{
    [SerializeField]
    private float base_hp_;
    public float base_hp { get => base_hp_; }

    [SerializeField]
    private float base_hp_regen_;
    public float base_hp_regen { get => base_hp_regen_; }

    [SerializeField]
    private float base_mp_;
    public float base_mp { get => base_mp_; }

    [SerializeField]
    private float base_mp_regen_;
    public float base_mp_regen { get => base_mp_regen_; }

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
}
