using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image item_image_;
    [SerializeField]
    private ItemType item_type_;
    public ItemType item_type { get => item_type_; }
    [SerializeField]
    private int item_index_;
    public int item_index { get => item_index_; }

    public void updateEquipSlot()
    {
        var item = PlayerManager.instance.playable.playable_equipment.getEquipItem(item_type_, item_index_);

        if (item != null)
        {
            if(item.item_data.item_type != ItemType.NONE)
            {
                item_image_.color = Color.white;
                item_image_.sprite = item.item_data.item_sprite;
            }
            else
            {
                item_image_.color = Color.clear;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            PlayerManager.instance.playable.playable_equipment.unequipItem(item_type_, item_index);
            UIManager.instance.equip_ui.updateSlots();
        }
    }
}
