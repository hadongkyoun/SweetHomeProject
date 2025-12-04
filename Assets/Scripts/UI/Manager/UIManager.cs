
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private PlayerInput playerInput;
    // 플레이어와 UI의 상호작용을 관리하는 스크립트
    private bool uiOpen;


    private InventoryUI inventoryUI;


    private void Awake()
    {
        inventoryUI = GetComponentInChildren<InventoryUI>();
        if(GameObject.FindWithTag("Player").TryGetComponent<PlayerInput>(out PlayerInput playerInput))
        {
            this.playerInput = playerInput;
        }
    }

    public void Inventory()
    {
        bool isInventoryOpen = inventoryUI.InventoryTrigger();
        playerInput.enabled = !isInventoryOpen;


    }

}
