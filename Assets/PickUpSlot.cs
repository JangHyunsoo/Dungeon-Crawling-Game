using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUpSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image item_image_;
    [SerializeField]
    private Text item_name_;
    [SerializeField]
    private Text item_amount_;

    private DropItem drop_item_;

    public void clearSlot()
    {
        drop_item_ = null;
    }

    public void setSlot(DropItem _drop_item)
    {
        drop_item_ = _drop_item;
        item_image_.color = Color.white;
        item_image_.sprite = _drop_item.item.item_data.item_sprite;
        item_name_.text = _drop_item.item.item_data.item_name;
        item_amount_.text = _drop_item.item.amount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            PlayerManager.instance.playable.playable_inventory.addItem(drop_item_.item);
            Destroy(drop_item_.gameObject);
            gameObject.active = false;
        }
    }
    
}
