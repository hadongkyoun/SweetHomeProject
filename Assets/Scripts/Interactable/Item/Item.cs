using UnityEngine;



[RequireComponent(typeof(InteractUI))]
public class Item : MonoBehaviour, IInteractable, IItem
{
    // Scriptable Object : ItemInformation
    [SerializeField]
    private ItemInformation itemInformation;
    public ItemInformation ItemInformation { get { return itemInformation; } }
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

    public void UseItem()
    {
        //throw new System.NotImplementedException();
    }
}
