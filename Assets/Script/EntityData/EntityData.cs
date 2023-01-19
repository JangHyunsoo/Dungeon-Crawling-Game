using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : ScriptableObject
{
    [SerializeField]
    private string entity_name_;
    public string entity_name { get => entity_name_; }

    [SerializeField]
    private int index_no_;
    public int index_no { get => index_no_; }

    [SerializeField]
    private int base_hp_;
    public int base_hp { get => base_hp_; }

    [SerializeField]
    private float base_hp_regen_;
    public float base_hp_regen { get => base_hp_regen_; }

    [SerializeField]
    private int base_mp_;
    public int base_mp { get => base_mp_; }

    [SerializeField]
    private float base_mp_regen_;
    public float base_mp_regen { get => base_mp_regen_; }
}
