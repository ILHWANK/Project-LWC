using Script.Common.UI;
using Script.Core.UI;

public class InventoryPopup : UIPopup
{
    private void Start()
    {
        // UIManager.Instance.OnPopupOpened += OnPopupOpen;
    }
    
    public void Close()
    {
        UIManager.Instance.ClosePopup("InventoryPopup");
    }
}
