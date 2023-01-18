using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApSyatem : ApSystem
{
    private void Update()
    {
        if(is_ready_)
        {
            if(max_ap_count != cur_ap_count_)
            {
                cur_value += Time.deltaTime * ap_value_;
                
                if (cur_value >= max_value)
                {
                    cur_ap_count_++;
                    cur_value -= max_value;
                }
            }
        }
    }
}
