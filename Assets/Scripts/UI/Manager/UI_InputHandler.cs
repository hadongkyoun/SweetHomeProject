using UnityEngine;
using UnityEngine.InputSystem;

public class UI_InputHandler : MonoBehaviour
{
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            uiManager.Inventory();
        }
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            uiManager.Menu();
        }
    }
}
