using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : Item
{
    protected List<StatValuePair> addition_stat_list_ = new List<StatValuePair>();
    public List<StatValuePair> addition_stat_list { get => addition_stat_list_; }

    protected int enhanced_value_;
    public int enhanced_value { get => enhanced_value_; }

    private int addition_stat_size_;

    protected bool is_curse_;
    public bool is_curse { get => is_curse_; }

    public bool include_addition_stat { get => addition_stat_list_.Count != 0; }

    public EquipItem(ItemData _equip_item_data) : base(_equip_item_data)
    {
        enhanced_value_ = 0;

        if (setIncludeAddition())
        {
            setRandomStat();
        }
    }

    protected virtual bool setIncludeAddition() { return false; }

    protected virtual void setRandomStat() { }
}