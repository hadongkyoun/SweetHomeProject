using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Space(15)]
    [Header("Main Slot Information")]
    [SerializeField]
    private TextMeshProUGUI nameTMP;
    [SerializeField]
    private TextMeshProUGUI contextTMP;
    [SerializeField]
    private TextMeshProUGUI slotIndicatorInfoTMP;

    [SerializeField]
    private CanvasGroup UIGuideGroup;

    private Animation mainSlotAnim;

    private SlotViewHandler slotViewHandler;

    private bool canClick = false;

    
    private void Awake()
    {
        slotViewHandler = FindFirstObjectByType<SlotViewHandler>();
    }

    private void Start()
    {
        slotIndicatorInfoTMP.text = $"";
        nameTMP.text = "";
        contextTMP.text = "";

        mainSlotAnim = GetComponent<Animation>();
    }
    public void PlayAnimation(string type)
    {
        mainSlotAnim.Play(type);
    }

    public void UpdateMainSlotItemInfo(List<Item> showingItems, int slotIndicator)
    {

        if (showingItems.Count == 0)
        {
            slotIndicatorInfoTMP.text = $"0/{showingItems.Count}";
            nameTMP.text = "";
            contextTMP.text = "";
        }
        else
        {
            slotIndicatorInfoTMP.text = $"{slotIndicator + 1}/{showingItems.Count}";
            nameTMP.text = showingItems[slotIndicator].GetItemName();
            contextTMP.text = showingItems[slotIndicator].GetItemContext();
        }
    }

    public void NoneImage()
    {
        slotIndicatorInfoTMP.text = "";
        nameTMP.text = "";
        contextTMP.text = "";

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIGuideGroup.alpha = 0.52f;
        canClick = true;
        // Mouse Pointer Image Change => Magnifer(µ¸º¸±â)
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIGuideGroup.alpha = 0.1f;
        canClick = false;
        // Mouse Pointer Image Change => basic
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(canClick)
        {
            slotViewHandler.Investigate();
        }
    }

    public void ResetEvent()
    {
        canClick = false;
        UIGuideGroup.alpha = 0;
    }
}
