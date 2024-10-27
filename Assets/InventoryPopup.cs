using System.Collections;
using System.Collections.Generic;
using script.Common;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : UIPopup
{
    public void Close()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
