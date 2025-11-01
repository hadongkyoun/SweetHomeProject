using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTrackingState : EnemyBaseState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;

    private GameObject player;
    private NavMeshAgent navMeshAgent;

    private bool setPath;

    public override void EnterState(EnemyAI enemyAI, EnemyData enemyData)
    {
        if (this.enemyAI == null && this.enemyData == null)
        {
            this.enemyAI = enemyAI;
            player = enemyAI.Player;
            this.enemyData = enemyData;
        }
        if(navMeshAgent == null && enemyAI.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            navMeshAgent = agent;
        }
        setPath = false;
        enemyAI.SetAnimParameterSpeed(enemyData.TrackingSpeed);
    }


    public override void UpdateState()
    {
        //Vector3 dir = (player.transform.position - enemyAI.transform.position).normalized;
        
        Quaternion targetRotation = Quaternion.LookRotation(navMeshAgent.desiredVelocity);

        if(navMeshAgent.pathPending == false)
            navMeshAgent.SetDestination(player.transform.position);

        enemyAI.transform.rotation = Quaternion.Lerp(enemyAI.transform.rotation, targetRotation, enemyData.RotationSpeed*Time.deltaTime);

        if (!enemyAI.PlayerDetected)
            enemyAI.SwitchState(enemyAI.RoamingState);
    }
    public override void ExitState()
    {
        
    }
}
