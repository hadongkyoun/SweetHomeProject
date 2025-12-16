using UnityEngine;
using UnityEngine.UI;

public enum InventoryListType
{
    Item,
    Equipment,
    Memo,
}

public class InventoryNameList : MonoBehaviour, IInventoryCloseReset
{
    private InventoryUI inventoryUI;

    private InventoryTypeButton[] buttons;

    private void Awake()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
        buttons = GetComponentsInChildren<InventoryTypeButton>();
    }

    public void ListSelected(InventoryListType listType)
    {
        inventoryUI.ChangeShowingItemList(listType);
    }

    public void ResetBtnExceptThis(InventoryTypeButton thisBtn)
    {
        foreach(InventoryTypeButton button in buttons)
        {
            if (button.isSelected && button != thisBtn)
            {
                button.ResetButton();
            }
        }
    }

    public void InventoryReset()
    {
        foreach (InventoryTypeButton button in buttons)
        {
            button.ResetButton();
        }
    }
}
