using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData enemy_data_;
    public EnemyData enemy_data { get => enemy_data_; }

    private Weapon weapon_;

    private int cur_hp_;
    public int cur_hp { get => cur_hp_; }

    public void initEnemyData(int _index)
    {
        enemy_data_ = EnemyDatabase.instance.getEnemyData(_index);

        cur_hp_ = enemy_data_.base_hp;
    }

    // 피격당하다 좋은 이름 추천...
    public void damaged(float _value)
    {
        cur_hp_ -= (int)_value;

        if(cur_hp <= 0)
        {
            // dead work
        }
    }

    public float getPhisicDamage()
    {
        // add weapon damage and buff, etc..
        return enemy_data_.physical_damage;
    }


}
