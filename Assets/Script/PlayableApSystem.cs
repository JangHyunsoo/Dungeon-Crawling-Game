using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableApSystem : ApSystem
{
    private void Start()
    {
        init(3);
    }

    private void Update()
    {
        //if(is_ready_)
        //{
        //    //if(max_ap_count != cur_ap_count_)
        //    {
        //        cur_value += Time.deltaTime * PlayerManager.instance.playable.playable_stat.getValue(StatType.ACTION_POINT);
        //
        //        if(cur_value >= max_value)
        //        {
        //            cur_ap_count_++;
        //            cur_value -= max_value;
        //        }
        //    }
        //}
    }

}
