using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData enemy_data_;
    public EnemyData enemy_data { get => enemy_data_; }

    [SerializeField]
    private EnemyAI enemy_ai_;
    public EnemyAI enemy_ai { get => enemy_ai_; }

    [SerializeField]
    private EnemyApSystem enemy_ap_system_;
    public EnemyApSystem enemy_ap_system { get => enemy_ap_system_; }

    private Weapon weapon_;

    private int cur_hp_;
    public int cur_hp { get => cur_hp_; }

    public void initEnemyData(int _index)
    {
       // enemy_data_ = EnemyDatabase.instance.getEnemyData(_index);
        
       // cur_hp_ = enemy_data_.base_hp;

        enemy_ap_system_.init(1);
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
