using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamingState : EnemyBaseState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;


    private Transform destination;
    private CharacterController enemyCharController;

    private NavMeshAgent navMeshAgent;
    private bool setPath = true;
    private NavMeshPath path;

    public override void EnterState(EnemyAI enemyAI, EnemyData enemyData)
    {
        if (this.enemyAI == null && this.enemyData == null)
        {
            this.enemyAI = enemyAI;
            this.enemyData = enemyData;
            enemyCharController = enemyAI.GetComponent<CharacterController>();
        }

        if (navMeshAgent == null && enemyAI.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            navMeshAgent = agent;
        }

        destination = this.enemyAI.GetRoamingPoint();


        // 그냥 destination 정하는걸로바꾸기
        path = new NavMeshPath();
        navMeshAgent.CalculatePath(destination.position, path);
        navMeshAgent.SetPath(path);
        path = null;
        setPath = false;
        enemyAI.SetAnimParameterSpeed(enemyData.RoamingSpeed);

    }

    public override void UpdateState()
    {

        Debug.Log("Roaming State");
        Vector3 dir = (destination.position - enemyAI.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dir);



        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            enemyAI.SwitchState(enemyAI.IdleState);
        }


        if (enemyAI.PlayerDetected)
            enemyAI.SwitchState(enemyAI.TrackingState);

        enemyAI.transform.rotation = Quaternion.Lerp(enemyAI.transform.rotation, targetRotation, enemyData.RotationSpeed * Time.deltaTime);
    }
    public override void ExitState()
    {
    }
}
