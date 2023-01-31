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

    private int item_data_index_;

    public void setItemSlotData(int _index)
    {
        item_data_index_ = _index;
        var item = PlayerManager.instance.playable.playable_inventory.getItemToIndexNum(_index);

        if(item != null)
        {
            item_image_.color = Color.white;
            item_image_.sprite = item.item_data.item_sprite;
            item_amount_.text = item.amount.ToString();
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
            Debug.Log(item_data_index_);
        }
    }
}
