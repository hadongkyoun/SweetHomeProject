using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



public class InventoryUI : MonoBehaviour
{
    private CanvasGroup Inventory;
    private List<Item> items;
    [SerializeField]
    private Slot[] ItemsSlot = new Slot[11];
    private int itemIndicator = 0;
    private int slotIndicator = 0;
    private int slotMoveDirection;
    [Space(15)]
    [Header("Player UI Handler State")]
    [SerializeField]
    private float wheelIncreaseAmount = 0.0f;
    private float wheelAmountPlus = 0;
    private float wheelAmountMinus = 0;


    private int referenceValue;
    private void Awake()
    {
        Inventory = GetComponent<CanvasGroup>();
        Inventory.alpha = 0.0f;
        Inventory.interactable = false;
        Inventory.blocksRaycasts = false;

    }

    private void Start()
    {
        items = FindFirstObjectByType<InventoryHandler>().GetItems();
    }

    private void Update()
    {
        if (!isOn)
            return;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        slotMoveDirection = 0;
        if (scrollInput != 0)
        {
            if (scrollInput < 0)
            {
                wheelAmountPlus = 0.0f;
                wheelAmountMinus += wheelIncreaseAmount * scrollInput;
            }
            else if (scrollInput > 0)
            {
                wheelAmountMinus = 0.0f;
                wheelAmountPlus += wheelIncreaseAmount * scrollInput;
            }


            if (wheelAmountPlus > 1.0f)
            {
                // this is left slot move
                wheelAmountPlus = 0.0f;
                slotMoveDirection = -1;
            }
            else if (wheelAmountMinus < -1.0f)
            {
                // this is right slot move
                wheelAmountMinus = 0.0f;
                slotMoveDirection = 1;
            }
            else
            {
                slotMoveDirection = 0;
            }

            itemIndicator += slotMoveDirection;

            if (itemIndicator <= 0)
                itemIndicator = 0;

            else if (itemIndicator >= items.Count - 1)
            {
                itemIndicator = items.Count - 1;
            }

            if (slotMoveDirection != 0)
            {
                UpdateSlot(true);
            }

        }
    }

    private bool isOn = false;
    public bool InventoryTrigger()
    {
        isOn = !isOn;

        if (isOn)
        {
            Inventory.alpha = 1;
        }
        else
        {
            Inventory.alpha = 0;
        }

        Inventory.blocksRaycasts = isOn;
        Inventory.interactable = isOn;

        return isOn;
    }

    public void UpdateItemSlotList()
    {
        if (items == null)
        {
            items = FindFirstObjectByType<InventoryHandler>().GetItems();
        }
        else
        {
            // Update slot visual terms.
            UpdateSlot(false);
        }
    }

    private void UpdateSlot(bool isScrolling)
    {
        if (items.Count <= 0)
            return;

        ItemsSlot[ItemsSlot.Length / 2].UpdateSlotImage(items[itemIndicator].ItemInformation);

        if (isScrolling)
        {
            if ((slotIndicator == 0 && slotMoveDirection == -1) || (slotIndicator == items.Count - 1 && slotMoveDirection == 1))
            {
                if (ItemsSlot[ItemsSlot.Length / 2].TryGetComponent<Animation>(out Animation animation))
                {
                    animation.Play("CannotMove");
                }
            }
            else
            {
                if (ItemsSlot[ItemsSlot.Length / 2].TryGetComponent<Animation>(out Animation animation))
                {
                    animation.Play("MainSlot");
                }
            }
        }

        // For item list index
        referenceValue = itemIndicator - ItemsSlot.Length / 2;

        for (int i = ItemsSlot.Length / 2; i >= 0; i--)
        {
            // i+referenceValue => item list index
            if (i + referenceValue < 0)
            {
                ItemsSlot[i].UpdateSlotImage(null);
            }
            else
            {
                ItemsSlot[i].UpdateSlotImage(items[i + referenceValue].ItemInformation);
            }
        }

        for (int i = ItemsSlot.Length / 2 + 1; i < ItemsSlot.Length; i++)
        {
            if (i + referenceValue >= items.Count)
            {
                ItemsSlot[i].UpdateSlotImage(null);
            }
            else
            {
                ItemsSlot[i].UpdateSlotImage(items[i + referenceValue].ItemInformation);
            }
        }
        slotIndicator = itemIndicator;
    }

}
