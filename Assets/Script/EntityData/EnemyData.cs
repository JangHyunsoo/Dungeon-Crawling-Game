using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : EntityData
{
    [SerializeField]
    private int physical_damage_;
    public int physical_damage { get => physical_damage_; }

    [SerializeField]
    private int armor_value_;
    public int armor_value { get => armor_value_; }

    [SerializeField]
    private float ap_value_;
    public float ap_value { get => ap_value_; }

    // 마법 종류, 데미지, 회피, 저항 등등 추가..
}
