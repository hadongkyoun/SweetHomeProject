
using Unity.Cinemachine;
using UnityEngine;

/*
    This is State Manager ( Final State Machine )
 */
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IGameSetting
{
    [Header("Player Camera Parameters : Local position")]
    [SerializeField]
    private Transform FollowCam;
    private Vector3 camIdleOffset;
    // 0.43 Z
    [SerializeField]
    private Vector3 camWalkOffset;
    [SerializeField]
    private Vector3 camSprintOffset;
    [SerializeField]
    private Vector3 camCrouchOffset;
    // 0.3 1.4 0.277
    //[SerializeField]
    //private Vector3 camGlimpseOffset;
    private Vector3 finalCamOffset;

    // This is not reference with above values
    private CinemachineCamera playerVirtualCamera;

    [Space(15)]
    // ================================================== Move
    [Header("Movement Parameters")]
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float sprintSpeed;
    private float currentSpeed;
    private Vector3 playerDirection;
    private bool isMoving;

    // ================================================== Look
    [Space(15)]
    [Header("Looking Parameters")]

    [Tooltip("마우스 민감도")]
    [SerializeField]
    private float sensitivityAmount;
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
    private float invertY;
    private int toggledInvertY;

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

        playerVirtualCamera = FindFirstObjectByType<CinemachineCamera>();
        if(playerVirtualCamera == null)
        {
            Debug.LogError("ERROR!! : Cannot reference virtual cinemachine camera in PlayerController.cs");
        }
    }

    private void Start()
    {
        toggledInvertY = PlayerPrefs.GetInt("invertY", 0);
        if (toggledInvertY == 1)
        {
            invertY = -1.0f;
        }
        else
        {
            invertY = 1.0f;
        }

        sensitivityAmount = PlayerPrefs.GetFloat("sensitivity", 0.2f);
        lookSensitivity = new Vector2(sensitivityAmount, sensitivityAmount);

        if (playerVirtualCamera != null)
        {
            playerVirtualCamera.Lens.FieldOfView = PlayerPrefs.GetFloat("fov", 60);
        }

        //=================================

        xVelocity_AnimParameter = Animator.StringToHash("X_Velocity");
        yVelocity_AnimParameter = Animator.StringToHash("Y_Velocity");

        camIdleOffset = FollowCam.transform.localPosition;
        finalCamOffset = FollowCam.transform.localPosition;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CheckInput();

        //CameraOffsetUpdate();

        GroundUpdate();
        GravityUpdate();


        MoveUpdate();
        LookUpdate();

        FollowCam.localPosition = finalCamOffset;
    }



    private void GroundUpdate()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            // Force player to ground
            _velocity.y = -3f;
        }
    }

    // 모델 변경시 삭제 예정
    void CameraOffsetUpdate()
    {

        // camOffsetZ => 로 설정 변경
        if (inputHandler.IsSprinting)
        {
            finalCamOffset = new Vector3(finalCamOffset.x, finalCamOffset.y, camSprintOffset.z);
        }
        else
        {
            if (inputHandler.MoveInput != Vector2.zero)
            {
                finalCamOffset = new Vector3(finalCamOffset.x, finalCamOffset.y, camWalkOffset.z);
            }
            else
            {
                finalCamOffset = new Vector3(finalCamOffset.x, finalCamOffset.y, camIdleOffset.z);
            }
        }
    }

    void CheckInput()
    {
        currentSpeed = inputHandler.IsSprinting ? sprintSpeed : moveSpeed;


        if (inputHandler.MoveInput == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        if (inputHandler.IsSprinting)
        {
            finalCamOffset = camIdleOffset;
            if (inputHandler.MoveInput.y < 0)
            {
                currentSpeed = sprintSpeed / 2;
            }

        }


        //if (inputHandler.IsGlimpseRight)
        //{
        //    finalCamOffset = new Vector3(camGlimpseOffset.x, camGlimpseOffset.y, finalCamOffset.z);
        //    isGlimpsing = true;
        //    currentSpeed = glimpseSpeed;
        //}
        //else if (inputHandler.IsGlimpseLeft)
        //{
        //    finalCamOffset = new Vector3(-camGlimpseOffset.x, camGlimpseOffset.y, finalCamOffset.z);
        //    isGlimpsing = true;
        //    currentSpeed = glimpseSpeed;
        //}
        //else
        //{
        //    finalCamOffset = camIdleOffset;
        //    isGlimpsing = false;
        //}
    }

    void MoveUpdate()
    {
        // Decide speed
        playerDirection = transform.forward * inputHandler.MoveInput.y + transform.right * inputHandler.MoveInput.x;
        playerDirection.y = 0;
        AnimationUpdate(inputHandler.MoveInput.x * currentSpeed, inputHandler.MoveInput.y * currentSpeed);

        playerDirection.Normalize();

        characterController.Move(playerDirection * currentSpeed * Time.deltaTime);
    }

    void AnimationUpdate(float xVel, float yVel)
    {

        animationVelocity.x = Mathf.Lerp(animationVelocity.x, xVel, animationBlendSpeed * Time.deltaTime);
        animationVelocity.y = Mathf.Lerp(animationVelocity.y, yVel, animationBlendSpeed * Time.deltaTime);

        _animator.SetFloat(xVelocity_AnimParameter, animationVelocity.x);
        _animator.SetFloat(yVelocity_AnimParameter, animationVelocity.y);

    }

    void GravityUpdate()
    {
        if (_isGrounded == false)
        {
            _velocity.y += gravity * Time.deltaTime;
        }
        characterController.Move(_velocity * Time.deltaTime);
    }



    void LookUpdate()
    {
        Vector2 lookInputValue = new Vector2(inputHandler.LookInput.x * lookSensitivity.x, inputHandler.LookInput.y * lookSensitivity.y);

        // Invert Y Check
        CurrentPitch -= lookInputValue.y * invertY;

        FollowCam.localRotation = Quaternion.Euler(CurrentPitch, 0, 0);
        transform.Rotate(Vector3.up * lookInputValue.x);

    }

    public void UpdateGameSettingInGame()
    {
        toggledInvertY = PlayerPrefs.GetInt("invertY", 0);
        if (toggledInvertY == 1)
        {
            invertY = -1.0f;
        }
        else
        {
            invertY = 1.0f;
        }

        sensitivityAmount = PlayerPrefs.GetFloat("sensitivity", 0.2f);
        lookSensitivity = new Vector2(sensitivityAmount, sensitivityAmount);

        playerVirtualCamera.Lens.FieldOfView = PlayerPrefs.GetFloat("fov", 60);
    }
}
