using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamingState : EnemyBaseState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;


    private Transform destination;
    private CharacterController enemyCharController;

    public override void EnterState(EnemyAI enemyAI, EnemyData enemyData)
    {
        if (this.enemyAI == null && this.enemyData == null)
        {
            this.enemyAI = enemyAI;
            this.enemyData = enemyData;
            enemyCharController = enemyAI.GetComponent<CharacterController>();
        }

        destination = this.enemyAI.GetRoamingPoint();
        enemyAI.SetAnimParameterSpeed(enemyData.RoamingSpeed);
    }

    public override void UpdateState()
    {
        Debug.Log("Roaming State");
        Vector3 dir = (destination.position - enemyAI.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dir);


        enemyAI.transform.rotation = Quaternion.Lerp(enemyAI.transform.rotation, targetRotation, enemyData.RotationSpeed * Time.deltaTime);

        enemyCharController.Move(dir * enemyData.RoamingSpeed * Time.deltaTime);

        
        if(Vector3.Distance(destination.position, enemyAI.transform.position) < 0.1f)
        {
            enemyAI.SwitchState(enemyAI.IdleState);
        }

        if (enemyAI.PlayerDetected)
            enemyAI.SwitchState(enemyAI.TrackingState);
    }
    public override void ExitState()
    {

    }
}
