using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApSystem : ApSystem
{
    private EnemyAI enemy_ai_;

    public override void init(int _max_ap_count)
    {
        base.init(_max_ap_count);
        enemy_ai_ = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        if (is_ready_)
        {
            cur_value += Time.deltaTime * ap_value_;

            if (cur_value >= max_value)
            {
                enemy_ai_.act();
                cur_value -= max_value;
            }
        }
    }
}