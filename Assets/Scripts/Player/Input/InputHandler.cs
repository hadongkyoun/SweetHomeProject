using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    //[Header("Player conference")]
    //[SerializeField]
    //private InventoryHandler inventoryHandler;

    private Vector2 moveInput;
    public Vector2 MoveInput { get { return moveInput; } }
    private Vector2 lookInput;
    public Vector2 LookInput { get { return lookInput; } }

    private bool isSprinting;
    public bool IsSprinting { get { return isSprinting; } }


    //private bool isGlimpseRight;
    //public bool IsGlimpseRight { get { return isGlimpseRight; } }

    //private bool isGlimpseLeft;
    //public bool IsGlimpseLeft { get { return isGlimpseLeft; } }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(TryGetComponent<Interactor>(out Interactor interactor))
            {
                interactor.TryInteract();
            }
        }
    }

    //public void OnInventory(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        inventoryHandler.InventoryTrigger();
    //    }
    //}


    //public void OnGlimpseRight(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        isGlimpseLeft = false;
    //        isGlimpseRight = true;
    //    }
    //    else
    //    {
    //        isGlimpseRight = false;
    //    }
    //}

    //public void OnGlimpseLeft(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        isGlimpseRight = false;
    //        isGlimpseLeft = true;

    //    }
    //    else
    //    {
    //        isGlimpseLeft = false;
    //    }
    //}
}
