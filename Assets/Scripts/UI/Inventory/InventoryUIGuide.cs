using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUIGuide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private CanvasGroup guideUIGroup;

    private bool isActivated = false;

    public void ActivateUIGuide(bool isOn)
    {
        isActivated = isOn;
        if (isActivated)
            guideUIGroup.alpha = 0.52f;
        else
            guideUIGroup.alpha = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActivated)
            guideUIGroup.alpha = 0.52f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        guideUIGroup.alpha = 0f;
    }



    // Make Investigate System here


}
