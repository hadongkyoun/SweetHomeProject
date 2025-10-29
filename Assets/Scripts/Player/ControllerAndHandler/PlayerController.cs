
using UnityEngine;

/*
    This is State Manager ( Final State Machine )
 */
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Value
    [Header("Movement Parameters")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float sprintSpeed;
    [Space(15)]
    [Header("Looking Parameters")]
    [SerializeField]
    private Transform TargetCamTransform;
    [SerializeField]
    private Vector2 lookSensitivity = new Vector2(0.1f, 0.1f);
    [SerializeField]
    private float pitchLimit = 85f;
    [SerializeField]
    private float currentPitch;
    public float CurrentPitch
    {
        get => currentPitch;
        set
        {
            currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
        }
    }





    #region Internal Value
    private bool isMoving;
    // Controller
    private InputHandler inputHandler;
    public InputHandler InputHandler { get { return inputHandler; } }
    private CharacterController characterController;
    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;   
    }

    void Update()
    {
        CheckInput();
        
        MoveUpdate();
        LookUpdate();
    }



    void CheckInput()
    {
        if(inputHandler.MoveInput == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    void MoveUpdate()
    {
        Vector3 direction = transform.forward * inputHandler.MoveInput.y + transform.right * inputHandler.MoveInput.x;
        direction.y = 0;
        direction.Normalize();

        characterController.Move(direction * moveSpeed * Time.deltaTime);
    }

    void LookUpdate()
    {
        Vector2 lookInputValue = new Vector2(inputHandler.LookInput.x * lookSensitivity.x, inputHandler.LookInput.y * lookSensitivity.y);

        CurrentPitch -= lookInputValue.y;

        TargetCamTransform.localRotation = Quaternion.Euler(CurrentPitch, 0, 0);

        transform.Rotate(Vector3.up * lookInputValue.x);
    }
}
