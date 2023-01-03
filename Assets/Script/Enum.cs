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

    // ���� �߰�

    // ���⿡ ����� �ɼ� �ִ� ���ݵ�

    // �ƴ� ���ݵ�

    PHYSICAL_DAMAGE,
    CRITICAL_CHANCE,
    CRITICAL_DAMAGE,
    HIT_ACCURACY,
    ACTION_POINT,
    CASTING_CHANCE
}
