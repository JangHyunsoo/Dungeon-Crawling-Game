using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableEquipment : MonoBehaviour
{
    private Weapon weapon_;
    public Weapon weapon { get => weapon_; }

    private Armor armor_;
    public Armor armor { get => armor_; }

    private Ring[] ring_arr_ = new Ring[2];
    public Ring[] ring_arr { get => ring_arr_; }
    

}
