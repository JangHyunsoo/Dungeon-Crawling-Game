using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApSystem : MonoBehaviour
{
    protected bool is_ready_ = false;

    protected int cur_ap_count_;
    public int cur_ap_count { get => cur_ap_count_; }

    protected int max_ap_count_;
    public int max_ap_count { get => max_ap_count_; }

    protected float ap_value_ = 1f;
    public float ap_value { get => ap_value_; }

    protected float cur_value = 0f;
    protected float max_value = 1f;
    
    public virtual void init(int _max_ap_count)
    {
        is_ready_ = true;
        cur_ap_count_ = 0;
        max_ap_count_ = _max_ap_count;
    }
}
