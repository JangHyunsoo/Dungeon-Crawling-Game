using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuUI : MonoBehaviour
{
    private Transform[] panel_ui_arr_;

    // Start is called before the first frame update
    void Start()
    {
        panel_ui_arr_ = Utility.getChildsTransform(transform);
    }

    public void clickMenuSwapButton(int _idx)
    {
        swapMenu(_idx);
    }

    private void swapMenu(int _idx)
    {
        for (int i = 0; i < panel_ui_arr_.Length; i++)
        {
            panel_ui_arr_[i].gameObject.active = i == _idx;
        }
    }
}
