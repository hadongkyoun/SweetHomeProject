using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [Space(15)]
    [Header("Enemy Roaming")]
    [SerializeField]
    private float maxStayTime;
    [SerializeField]
    private float minStayTime;
    [SerializeField]
    private Transform[] roamingPoints;

    [Space(15)]
    [Header("Enemy Move : It have to be same with Animator ThresHold!!!")]
    [SerializeField]
    private float roamingSpeed;
    public float RoamingSpeed { get { return roamingSpeed; } }
    [SerializeField]
    private float trackingSpeed;
    public float TrackingSpeed { get { return trackingSpeed; } }
    [SerializeField]
    private float rotationSpeed;
    public float RotationSpeed
    {
        get { return rotationSpeed; }
    }


    public bool PlayerDetected;
    public GameObject Player;

    private NavMeshAgent agentAI;
    public NavMeshAgent AgentAI { get { return agentAI; } }

    // FSM
    private EnemyBaseState currentState;
    public EnemyRoamingState RoamingState = new EnemyRoamingState();
    public EnemyTrackingState TrackingState = new EnemyTrackingState();

    private Animator animator;
    private int speedParameter;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        agentAI = GetComponent<NavMeshAgent>();
        agentAI.updateRotation = false;
        animator = GetComponentInChildren<Animator>(); 
        
    }

    void Start()
    {
        speedParameter = Animator.StringToHash("Speed");

        currentState = RoamingState;
        currentState.EnterState(this);

        this.transform.position = roamingPoints[Random.Range(0, roamingPoints.Length)].position;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState.ExitState();
        currentState = state;
        state.EnterState(this);
    }


    #region RoamingState
    public Transform GetRoamingPoint()
    {
        if(roamingPoints.Length == 0)
        {
            return null;
        }
        return roamingPoints[Random.Range(0, roamingPoints.Length)];
    }

    public float GetStayTime()
    {
        return Random.Range(minStayTime, maxStayTime);
    }

    #endregion

    #region TrackingState

    #endregion


    public void SetStateSpeed(float speed)
    {
        animator.SetFloat(speedParameter, speed);
        agentAI.speed = speed;
    }
    public void SetSpeedParameter(float speed)
    {
        animator.SetFloat(speedParameter, speed);
    }

}
