using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;



public class InventoryUI : MonoBehaviour
{
    private CanvasGroup Inventory;



    private List<Item> items;
    // Real visual
    private List<Item> ForShowingTempList = new List<Item>();
    private List<Item> showingItems;


    // 켜질 때, 이거 확인
    private InventoryListType currentItemListType = InventoryListType.Item;

    [SerializeField]
    private Slot[] ItemsSlot = new Slot[11];

    private int itemIndicator = 0;
    private int slotIndicator = 0;
    private int slotMoveDirection;

    [Header("Inventory Slot Visual")]
    [SerializeField]
    private CanvasGroup itemSlotView;
    [SerializeField]
    private SlotViewHandler slotViewHandler;
    private InvestigateHandler investigateHandler;

    [Space(15)]
    [Header("Player UI Handler State")]
    [SerializeField]
    private float wheelIncreaseAmount = 0.0f;
    private float wheelAmountPlus = 0;
    private float wheelAmountMinus = 0;



    private int referenceValue;
    private bool itemSlotSystemOn = false;

    private UnityEvent ResetEvent = new UnityEvent();

    private bool resetFinish = false;

    private void Awake()
    {
        Inventory = GetComponent<CanvasGroup>();
        Inventory.alpha = 0.0f;
        Inventory.interactable = false;
        Inventory.blocksRaycasts = false;

        investigateHandler = GetComponentInChildren<InvestigateHandler>();

        IEnumerable<IInventoryCloseReset> allResetObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IInventoryCloseReset>().ToList();

        Debug.Log($"[InventoryUI] : Found {allResetObjects.Count()} IInventoryCloseReset objects.");

        foreach (var resetObject in allResetObjects)
        {
            ResetEvent.AddListener(resetObject.InventoryReset);
        }

    }

    private void Start()
    {
        items = FindFirstObjectByType<InventoryHandler>().GetItems();
        showingItems = ForShowingTempList;
        // Practical item list : showing Items
        SetShowingItemList();


    }


    private void Update()
    {


        if (!isOn)
        {
            if (resetFinish == false)
            {
                ResetEvent?.Invoke();
                ItemsCountZero();
                resetFinish = true;
            }
            return;
        }
        resetFinish = false;



        #region Wheel Scroll Update



        if (itemSlotSystemOn)
        {
            if (investigateHandler.IsInvestigateModeOn())
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
        #endregion
    }

    private bool isOn = false;


    public Item GetIndicatedItem()
    {
        if (showingItems.Count == 0)
        {
            return null;
        }
        return showingItems[itemIndicator];
    }


    #region Inventory UI Handle Methods
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

    private void ItemsCountZero()
    {

        for (int i = 0; i < ItemsSlot.Length; i++)
        {
            ItemsSlot[i].UpdateSlotImage(null);
        }

        // Reset indicators
        itemIndicator = 0;
        slotIndicator = 0;

        slotViewHandler.ActivateUIGuide(false);
        itemSlotSystemOn = false;

        Debug.Log("Items count is zero.");
    }
    #endregion

    #region Update Slot Methods

    // Execute only once from InventoryHandler.cs when item pick up.
    public void UpdateItemSlotList()
    {
        if (items == null)
        {
            Debug.LogError("[Error] : There is no item list reference");
        }
        else
        {
            SetShowingItemList();
            // Update slot visual terms.
            UpdateSlot(false);
        }
    }

    private void UpdateSlot(bool isScrolling)
    {
        if (showingItems.Count <= 0)
        {
            ItemsCountZero();
            return;
        }

        if (!isOn)
        {
            return;
        }


        // Item UI Guide on
        slotViewHandler.ActivateUIGuide(true);

        ItemsSlot[ItemsSlot.Length / 2].UpdateSlotImage(showingItems[itemIndicator].GetItemSprite());

        if (isScrolling)
        {
            if ((slotIndicator == 0 && slotMoveDirection == -1) || (slotIndicator == showingItems.Count - 1 && slotMoveDirection == 1))
            {
                if (ItemsSlot[ItemsSlot.Length / 2].TryGetComponent<MainSlot>(out MainSlot mainSlot))
                {
                    mainSlot.PlayAnimation("CannotMove");
                }
            }
            else
            {
                if (ItemsSlot[ItemsSlot.Length / 2].TryGetComponent<MainSlot>(out MainSlot mainSlot))
                {
                    mainSlot.PlayAnimation("MainSlot");
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
                ItemsSlot[i].UpdateSlotImage(showingItems[i + referenceValue].GetItemSprite());
            }
        }

        for (int i = ItemsSlot.Length / 2 + 1; i < ItemsSlot.Length; i++)
        {
            if (i + referenceValue >= showingItems.Count)
            {
                ItemsSlot[i].UpdateSlotImage(null);
            }
            else
            {
                ItemsSlot[i].UpdateSlotImage(showingItems[i + referenceValue].GetItemSprite());
            }
        }

        slotIndicator = itemIndicator;
        // Main Slot Index => ItemsSlot.Length / 2
        if (ItemsSlot[ItemsSlot.Length / 2].TryGetComponent<MainSlot>(out MainSlot mainSlotItem))
        {
            mainSlotItem.UpdateMainSlotItemInfo(showingItems, slotIndicator);
        }

    }
    #endregion


    #region Showing Item List Methods
    private void SetShowingItemList()
    {
        ForShowingTempList.Clear();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].GetItemType() == currentItemListType)
            {
                ForShowingTempList.Add(items[i]);
            }
        }
    }

    public void ChangeShowingItemList(InventoryListType listType)
    {
        if (items.Count <= 0)
        {
            ItemsCountZero();
            return;
        }



        itemSlotSystemOn = true;
        currentItemListType = listType;

        // Reset indicators
        itemIndicator = 0;
        slotIndicator = 0;


        SetShowingItemList();
        // Update slot visuals
        UpdateSlot(false);

        itemSlotView.alpha = 1.0f;

    }
    #endregion


}
