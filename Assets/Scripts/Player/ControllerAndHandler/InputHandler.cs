using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    private Vector2 moveInput;
    public Vector2 MoveInput { get { return moveInput; } }
    private Vector2 lookInput;
    public Vector2 LookInput { get { return lookInput; } }

    private bool isSprinting;
    public bool IsSprinting { get { return isSprinting; } }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput =  context.ReadValue<Vector2>();
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
}
