using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weapon data", menuName = "ItemData/WeaponData", order = 2)]
public class WeaponData : ItemData 
{
    [SerializeField]
    private WeaponType weapon_type_;
    public WeaponType weapon_type { get => weapon_type_; }

    [SerializeField]
    private float base_damage_;
    public float base_damage { get => base_damage_; }

    [SerializeField]
    private float base_speed_;
    public float base_speed { get => base_speed_; }

    [SerializeField]
    private float base_accuracy_;
    public float base_accuracy { get => base_accuracy_; }

    // tier, rarlity, drop chance 등 추가사항
}
