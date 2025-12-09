using UnityEngine;
using UnityEngine.UI;

public enum InventoryListType
{
    Item,
    Equipment,
    Memo,
}

public class InventoryNameList : MonoBehaviour
{
    private InventoryUI inventoryUI;

    private void Awake()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();

    }

    public void ListSelected(InventoryListType listType)
    {
        inventoryUI.ChangeShowingItemList(listType);
    }

}
