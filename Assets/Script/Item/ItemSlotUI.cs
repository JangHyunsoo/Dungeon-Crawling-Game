using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image item_image_;

    private int item_data_index_;

    public void setItemSlotData(int _index)
    {
        item_data_index_ = _index;

        item_image_.color = PlayerManager.instance.playerable_inventory.getItemToIndexNum(_index).item_data.temp_color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("test");
        }
    }
}
