using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image item_image_;
    [SerializeField]
    private Text item_amount_;

    [SerializeField]
    private int item_index_;

    public void setIndex(int _idx)
    {
        item_index_ = _idx;
    }

    public void setItemSlotData()
    {
        var item = PlayerManager.instance.playable.playable_inventory.getItemToIndexNum(item_index_);

        if(item != null)
        {
            item_image_.color = Color.white;
            item_image_.sprite = item.item_data.item_sprite;
            item_amount_.text = item.amount == 1 ? "" : item.amount.ToString();
        }
        else
        {
            item_image_.color = Color.clear;
            item_amount_.text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            var item = PlayerManager.instance.playable.playable_inventory.getItemToIndexNum(item_index_);
            
            if(item != null)
            {
                PlayerManager.instance.playable.playable_equipment.equipItem(item);
                UIManager.instance.equip_ui.updateSlots();
            }
        }
    }
}
