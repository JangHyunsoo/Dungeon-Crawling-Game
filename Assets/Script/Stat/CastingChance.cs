using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingChance : Stat
{
    public override float getValue()
    {
        //  int             ���� ����,
        //  magic_aptitude  ���� ����
        //  �Ƹ� ���� ���ؿ� ���� ������ �ޱ� ������ Stat���� ���� ������
        return PlayerManager.instance.playable.playable_stat.getValue(StatType.INTELLIGENT);
    }
}
