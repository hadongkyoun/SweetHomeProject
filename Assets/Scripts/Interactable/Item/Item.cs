using UnityEngine;



[RequireComponent(typeof(InteractUI))]
public class Item : MonoBehaviour, IInteractable
{
    // Scriptable Object : ItemInformation
    [SerializeField]
    private ItemInformation itemInformation;
    [SerializeField]
    private GameObject interactableUI;


    private bool canInteractWithPlayer;
    private InteractUI interactUI;
    private InventoryHandler inventoryHandler;

    private void Awake()
    {
        interactUI = GetComponent<InteractUI>();
        interactUI.Initialize(Instantiate(interactableUI, transform));
        inventoryHandler = FindFirstObjectByType<InventoryHandler>();
    }

    public Sprite GetItemSprite()
    {
        return itemInformation.Sprite;
    }

    public string GetItemName()
    {
        return itemInformation.ItemName;
    }
    public string GetItemContext()
    {
        return itemInformation.ItemContext;
    }
    public InventoryListType GetItemType()
    {
        return itemInformation.ItemType;
    }
    

    public void Interact()
    {
        if (!canInteractWithPlayer)
        {
            return;
        }

        if (inventoryHandler != null)
        {
            inventoryHandler.PlayerPickItem(this);
        }
        else
        {
            Debug.LogError("[Error] : There is no inventory system. Need Inventory Handler.cs on Inventory System.");
        }
    }

    public void Detected(bool isOn)
    {
        if (canInteractWithPlayer == isOn)
            return;
        interactUI.ActivateUI(isOn);

        canInteractWithPlayer = isOn;

    }

    //public void ActivateUse()
    //{

    //    if (GetItemCanUse() == false)
    //    {
    //        Debug.LogError("This is not can use item. But it called.");
    //        return;
    //    }

    //    inventoryHandler.PlayerUseItem(this);
    //}
}
