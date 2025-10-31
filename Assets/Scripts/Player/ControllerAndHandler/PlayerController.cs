
using UnityEngine;

/*
    This is State Manager ( Final State Machine )
 */
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // ================================================== Move
    [Header("Movement Parameters")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float sprintSpeed;
    private Vector3 playerDirection = Vector3.zero;

    public float animParameter_speed
    {
        set
        {
            animationHandler.SetCurrentSpeed(value);
            Debug.Log("Changed");
        }
    }

    // ================================================== Look
    [Space(15)]
    [Header("Looking Parameters")]
    [SerializeField]
    private Transform FollowCam;

    [Tooltip("마우스 민감도")]
    [SerializeField]
    private Vector2 lookSensitivity = new Vector2(0.1f, 0.1f);
    [Tooltip("상하 고개 한계점")]
    [SerializeField]
    private float pitchLimit = 85f;

    private float currentPitch;
    public float CurrentPitch
    {
        get => currentPitch;
        set
        {
            currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
        }
    }

    [SerializeField]
    private GameObject Head;




    #region Components
    // Controller
    private InputHandler inputHandler;
    public InputHandler InputHandler { get { return inputHandler; } }
    private CharacterController characterController;
    private PlayerAnimatorHandler animationHandler;
    #endregion

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        characterController = GetComponent<CharacterController>();
        animationHandler = GetComponentInChildren<PlayerAnimatorHandler>();
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

        //Debug.Log(Head.transform.position);
    }



    void CheckInput()
    {


    }

    void MoveUpdate()
    {
        playerDirection = transform.forward * inputHandler.MoveInput.y + transform.right * inputHandler.MoveInput.x;
        playerDirection.y = 0;
        playerDirection.Normalize();

        // Decide speed
        float speed = moveSpeed;
        if (inputHandler.IsSprinting)
        {
            if (inputHandler.MoveInput.y >= 0)
                speed = sprintSpeed;
        }

        characterController.Move(playerDirection * speed * Time.deltaTime);

        // Set animation parameter ( speed )
        animParameter_speed = playerDirection.magnitude * speed;
    }

    void LookUpdate()
    {
        Vector2 lookInputValue = new Vector2(inputHandler.LookInput.x * lookSensitivity.x, inputHandler.LookInput.y * lookSensitivity.y);

        CurrentPitch -= lookInputValue.y;

        FollowCam.localRotation = Quaternion.Euler(CurrentPitch, 0, 0);

        transform.Rotate(Vector3.up * lookInputValue.x);
    }

}
