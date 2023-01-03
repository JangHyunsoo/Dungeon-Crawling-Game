using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WEAPON,
    ARMOR,
    RING,
    NECKLESS,
    POTION,
    SCROLL,
    OTHER
}

public enum WeaponType
{
    SWORD,
    AXE,
    SPEAR,
    BLUNT
}

public enum StatType 
{
    MAX_HP,
    HP_REGEN,
    MAX_MP,
    MP_REGEN,
    STRENGTH,
    INTELLIGENT,
    DEXTERITY,
    ARMOR_VALUE,

    // 내성 추가

    // 무기에 에디션 될수 있는 스텟들

    // 아닌 스텟들

    PHYSICAL_DAMAGE,
    CRITICAL_CHANCE,
    CRITICAL_DAMAGE,
    HIT_ACCURACY,
    ACTION_POINT,
    CASTING_CHANCE
}
