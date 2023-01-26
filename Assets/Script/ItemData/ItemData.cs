using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [SerializeField]
    private string item_name_;
    public string item_name { get => item_name_; }

    [SerializeField]
    private int index_no_;
    public int index_no { get => index_no_; }

    [SerializeField]
    private int item_rarlity_;
    public int item_rarlity { get => item_rarlity_; }


    [SerializeField]
    private ItemType item_type_;
    public ItemType item_type { get => item_type_; }

    [SerializeField]
    private Color temp_color_;
    public Color temp_color { get => temp_color_; }

}
