using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private EnemyAI enemyAI;
    private EnemyData enemyData;

    private float stayTime;
    private float currentTime;

    public override void EnterState(EnemyAI enemyAI, EnemyData enemyData)
    {
        if(this.enemyAI == null && this.enemyData == null)
        {
            this.enemyAI = enemyAI;
            this.enemyData = enemyData;
            stayTime = enemyData.GetRandomStayTime();
        }
        currentTime = 0.0f;
        enemyAI.SetAnimParameterSpeed(0);
    }

    public override void UpdateState()
    {
        Debug.Log("Idle State");
        currentTime += Time.deltaTime;
        if (currentTime > stayTime)
        {
            enemyAI.SwitchState(enemyAI.RoamingState);
        }

        if (enemyAI.PlayerDetected)
            enemyAI.SwitchState(enemyAI.TrackingState);
    }

    public override void ExitState()
    {
        
    }

    
}
