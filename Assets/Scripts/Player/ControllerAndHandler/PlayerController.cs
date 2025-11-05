
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
    private float currentSpeed;
    private Vector3 playerDirection;

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

    [Space(15)]
    [Header("Physics Parameters")]
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private LayerMask groundMask;

    private Vector3 _velocity;
    private bool _isGrounded;

    //[SerializeField]
    //private GameObject Head;

    [Space(15)]
    [Header("Animation Parameters")]
    [SerializeField]
    private float animationBlendSpeed;
    private Animator _animator;

    private int xVelocity_AnimParameter;
    private int yVelocity_AnimParameter;
    private Vector2 animationVelocity;

    #region Components
    // Controller
    private InputHandler inputHandler;
    public InputHandler InputHandler { get { return inputHandler; } }
    private CharacterController characterController;
    
    #endregion

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        xVelocity_AnimParameter = Animator.StringToHash("X_Velocity");
        yVelocity_AnimParameter = Animator.StringToHash("Y_Velocity");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            // Force player to ground
            _velocity.y = -3f;
        }

        CheckInput();

        MoveUpdate();

        PhysicsUpdate();

        LookUpdate();

    }



    void CheckInput()
    {
        currentSpeed = inputHandler.IsSprinting ? sprintSpeed : moveSpeed;
    }

    void MoveUpdate()
    {

        // Decide speed
        playerDirection = transform.forward * inputHandler.MoveInput.y + transform.right * inputHandler.MoveInput.x;
        playerDirection.y = 0;
        AnimationUpdate(inputHandler.MoveInput.x * currentSpeed, inputHandler.MoveInput.y * currentSpeed);

        playerDirection.Normalize();

        characterController.Move(playerDirection * currentSpeed* Time.deltaTime);


    }

    void AnimationUpdate(float xVel, float yVel)
    {

        animationVelocity.x = Mathf.Lerp(animationVelocity.x, xVel, animationBlendSpeed * Time.deltaTime);
        animationVelocity.y = Mathf.Lerp(animationVelocity.y, yVel, animationBlendSpeed * Time.deltaTime);

        _animator.SetFloat(xVelocity_AnimParameter, animationVelocity.x);
        _animator.SetFloat(yVelocity_AnimParameter, animationVelocity.y);

    }

    void PhysicsUpdate()
    {
        _velocity.y += gravity * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);
    }

    void LookUpdate()
    {
        Vector2 lookInputValue = new Vector2(inputHandler.LookInput.x * lookSensitivity.x, inputHandler.LookInput.y * lookSensitivity.y);

        CurrentPitch -= lookInputValue.y;

        FollowCam.localRotation = Quaternion.Euler(CurrentPitch, 0, 0);
        transform.Rotate(Vector3.up * lookInputValue.x);
    }


}
