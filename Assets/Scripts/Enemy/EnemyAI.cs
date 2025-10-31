using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyData))]
public class EnemyAI : MonoBehaviour
{
    public bool PlayerDetected;
    public GameObject Player;

    private EnemySystemHandler enemySystemHandler;
    private EnemyData enemyData;
    private EnemyVision enemyVision;
    private EnemyAnimationHandler enemyAnimationHandler;


    // FSM
    private EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyRoamingState RoamingState = new EnemyRoamingState();
    public EnemyTrackingState TrackingState = new EnemyTrackingState();


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyData = GetComponent<EnemyData>();
        enemyVision = FindFirstObjectByType<EnemyVision>();
        enemyAnimationHandler = GetComponentInChildren<EnemyAnimationHandler>();    
    }

    void Start()
    {
        enemyVision.SetValue(enemyData);
        currentState = IdleState;
        currentState.EnterState(this, enemyData);
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
        state.EnterState(this, enemyData);
    }

    public void SetEnemySystemHandler(EnemySystemHandler enemySystemHandler)
    {
        this.enemySystemHandler = enemySystemHandler;
    }

    public Transform GetRoamingPoint()
    {
        return enemySystemHandler.GetEnemyRoamingPoint();
    }

    public void SetAnimParameterSpeed(float speed)
    {
        enemyAnimationHandler.SetCurrentSpeed(speed);
    }
}
