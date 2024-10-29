using script.Common;

public class InventoryPopup : UIPopup
{
    private void Start()
    {
        // UIManager.Instance.OnPopupOpened += OnPopupOpen;
    }
    
    public void OnPopupOpen(UIPopup popup)
    {
        popup = this;
    }
    
    public void Close()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
