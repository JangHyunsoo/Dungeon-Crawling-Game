using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    NONE,
    WEAPON,
    ARMOR,
    RING,
    POTION,
    SCROLL,
    OTHER
}

public enum WeaponType
{
    SWORD = 0,
    AXE = 1,
    SPEAR = 2,
    BLUNT = 3
}

public enum ArmorType
{ 
    ROB,
    LEATHER,
    SCALE,
    PLATE
}

public enum RingType
{
    HPREGEN,
    MPREGEN,
    MAGICRES
}

public enum AptitudeType
{
    SWORD = 0,
    AXE = 1,
    SPEAR = 2,
    BLUNT = 3,

    FIRE_MAGIC,
    ICE_MAGIC
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