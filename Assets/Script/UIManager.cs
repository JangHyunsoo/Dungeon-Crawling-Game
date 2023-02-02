using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private InventoryUI inventory_ui_;
    public InventoryUI inventory_ui { get => inventory_ui_; }

    [SerializeField]
    private PickUpUI pickup_ui_;
    public PickUpUI pickup_ui { get => pickup_ui_; }

    [SerializeField]
    private EquipUI equip_ui_;
    public EquipUI equip_ui { get => equip_ui_; }
}