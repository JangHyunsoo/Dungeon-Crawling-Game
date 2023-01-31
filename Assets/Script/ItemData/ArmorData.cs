using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "armor data", menuName = "ItemData/ArmorData", order = 2)]
public class ArmorData : ItemData
{
    [SerializeField]
    private ArmorType armor_type_;
    public ArmorType armor_type { get => armor_type_; }
}
