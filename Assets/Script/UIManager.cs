using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject control_panel_;
    public GameObject control_panel { get { return control_panel_; } }

    [SerializeField]
    private GameObject inventory_panel_;
    public GameObject inventory_panel { get { return inventory_panel_; } }
    
    [SerializeField]
    private GameObject equip_panel_;
    public GameObject equip_panel { get { return equip_panel_; } }


    [SerializeField]
    private InventoryUI inventory_ui_;
    public InventoryUI inventory_ui { get => inventory_ui_; }

    [SerializeField]
    private PickUpUI pickup_ui_;
    public PickUpUI pickup_ui { get => pickup_ui_; }

    [SerializeField]
    private EquipUI equip_ui_;
    public EquipUI equip_ui { get => equip_ui_; }

    private MenuType cur_menu_ = MenuType.CONTROL;
    private Dictionary<MenuType, GameObject> menu_dict_ = new Dictionary<MenuType, GameObject>();

    public void init()
    {
        menu_dict_[MenuType.INVENTORY] = inventory_panel_;
        menu_dict_[MenuType.CONTROL] = control_panel_;
        menu_dict_[MenuType.EQUIPMENT] = equip_panel_;
        openMove();
    }

    private void openUI(MenuType _menu_type)
    {
        menu_dict_[cur_menu_].SetActive(false);
        cur_menu_ = _menu_type;
        menu_dict_[cur_menu_].SetActive(true);
    }

    public void openInventory()
    {
        openUI(MenuType.INVENTORY);
    }

    public void openMove()
    {
        openUI(MenuType.CONTROL);
    }

}