using UnityEngine;
using UnityEngine.EventSystems;

public class SlotViewHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Slot UI Guide Inspector")]
    [SerializeField]
    private CanvasGroup moveUIGuide;
    [SerializeField]
    private CanvasGroup clickUIGuide;

    private bool isActivated = false;

    [SerializeField]
    private InvestigateHandler investigateHandler;

    private InventoryUI inventoryUI;
    private void Awake()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();  
    }

    public void ActivateUIGuide(bool isOn)
    {
        isActivated = isOn;
        if (!isActivated)
        {
            moveUIGuide.alpha = 0f;
            clickUIGuide.alpha = 0f;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActivated)
        {
            moveUIGuide.alpha = 0.52f;
            clickUIGuide.alpha = 0.1f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        moveUIGuide.alpha = 0f;
        clickUIGuide.alpha = 0f;
    }

    public void Investigate()
    {
        if (!isActivated)
            return;

        Item item = inventoryUI.GetIndicatedItem();
        if (item == null)
            return;

        investigateHandler.InvestigateModeOn(item, true);
    }

}
