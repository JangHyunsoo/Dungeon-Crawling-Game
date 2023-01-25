using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private Item item_;
    public Item item { get => item_; }

    [SerializeField]
    private SpriteRenderer sr_;

    public void setItem(Item _item)
    {
        item_ = _item;
        sr_.color = item_.item_data.temp_color;
    }
}
