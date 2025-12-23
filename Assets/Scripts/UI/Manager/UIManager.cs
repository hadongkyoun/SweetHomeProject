
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject MenuPrefab;

    private PlayerInput playerInput;
    // 플레이어와 UI의 상호작용을 관리하는 스크립트
    private bool uiOpen;


    private InventoryUI inventoryUI;
    private CanvasGroup inventoryCanvasGroup;
    private bool inventoryOn;

    private MainMenuHandler mainMenuHandler;
    private CanvasGroup mainMenuCanvasGroup;
    private bool mainMenuOn;

    private void Awake()
    {
        inventoryUI = GetComponentInChildren<InventoryUI>();
        inventoryCanvasGroup = GetComponent<CanvasGroup>();

        if (GameObject.FindWithTag("Player").TryGetComponent<PlayerInput>(out PlayerInput playerInput))
        {
            this.playerInput = playerInput;
        }
        if (MenuPrefab != null)
        {
            GameObject mainMenuObject = Instantiate(MenuPrefab, this.transform);
            mainMenuHandler = mainMenuObject.GetComponent<MainMenuHandler>();
            mainMenuCanvasGroup = mainMenuObject.GetComponent<CanvasGroup>();
        }
    }

    private void LimitCursor(bool isOn)
    {
        if (isOn)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Inventory()
    {
        if (mainMenuOn)
            return;

        inventoryOn = !inventoryOn;
        if (inventoryOn)
        {
            inventoryCanvasGroup.alpha = 1;
        }
        else
        {
            inventoryCanvasGroup.alpha = 0;
        }

        inventoryCanvasGroup.blocksRaycasts = inventoryOn;
        inventoryCanvasGroup.interactable = inventoryOn;

        playerInput.enabled = !inventoryOn;
        LimitCursor(inventoryOn);
    }

    public void Menu()
    {
        mainMenuOn = !mainMenuOn;
        if (mainMenuOn)
        {
            mainMenuCanvasGroup.alpha = 1;
        }
        else
        {
            mainMenuCanvasGroup.alpha = 0;
        }

        mainMenuCanvasGroup.blocksRaycasts = mainMenuOn;
        mainMenuCanvasGroup.interactable = mainMenuOn;

        playerInput.enabled = !mainMenuOn;
        LimitCursor(mainMenuOn);
    }

}
