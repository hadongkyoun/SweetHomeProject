using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTypeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Image targetGraphicImage;
    private InventoryNameList inventoryNameList;

    public bool isSelected;

    [SerializeField]
    private InventoryListType listType;

    private Color highlightedColor;
    private Color selectedColor;

    private Color disappearColor;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(IsSelected);
        targetGraphicImage = GetComponentInChildren<Image>();
        inventoryNameList = GetComponentInParent<InventoryNameList>();

        highlightedColor = new Color32(150, 150, 150, 60);
        selectedColor = new Color32(220, 220, 220, 60);
        disappearColor = new Color32(245, 245, 245, 0);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // If not selected, no execut highlight effect
        if (!isSelected)
            targetGraphicImage.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If not selected, enalbe disappear effect
        if (!isSelected)
            targetGraphicImage.color = disappearColor;
    }

    public void ResetButton()
    {
        targetGraphicImage.color = disappearColor;
        isSelected = false;
    }

    private void IsSelected()
    {
        inventoryNameList.ResetBtnExceptThis(this);

        isSelected = true;
        targetGraphicImage.color = selectedColor;

        inventoryNameList.ListSelected(listType);
    }

}
