using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy data", menuName = "EntityData/EnemyData", order = 1)]
public class EnemyData : EntityData
{
    [SerializeField]
    private int physical_damage_;
    public int physical_damage { get => physical_damage_; }

    [SerializeField]
    private int armor_value_;
    public int armor_value { get => armor_value_; }

    [SerializeField]
    private float action_value_;
    public float action_value { get => action_value_; }

    [SerializeField]
    private int rarlity_;
    public int rarlity { get => rarlity_; }

    [SerializeField]
    private Color color_;
    public  Color color { get => color_; }

    [SerializeField]
    private Sprite sprite_;
    public Sprite sprite { get => sprite_; }

    // 마법 종류, 데미지, 회피, 저항 등등 추가..
}
