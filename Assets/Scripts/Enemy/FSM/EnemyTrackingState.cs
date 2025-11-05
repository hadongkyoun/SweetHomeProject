
using UnityEngine;
using UnityEngine.AI;

public class EnemyTrackingState : EnemyBaseState
{
    private EnemyAI _enemyAI;
    private Vector3 _enemyDirection;
    public override void EnterState(EnemyAI enemyAI)
    {
        if (_enemyAI == null)
        {
            _enemyAI = enemyAI;
        }

        _enemyAI.SetStateSpeed(_enemyAI.TrackingSpeed);
    }


    public override void UpdateState()
    {
        //Debug.Log($"[Tracking State] Speed : {navMeshAgent.speed}");
        

        if(_enemyAI.AgentAI.pathPending == false)
            _enemyAI.AgentAI.SetDestination(_enemyAI.Player.transform.position);

        if (_enemyAI.AgentAI.remainingDistance <= _enemyAI.AgentAI.stoppingDistance)
        {
            //_enemyAI.AgentAI.
        }
        else
        {
            _enemyDirection = _enemyAI.AgentAI.desiredVelocity;
        }


        Quaternion targetRotation = Quaternion.LookRotation(_enemyDirection);
        _enemyAI.transform.rotation = Quaternion.Lerp(_enemyAI.transform.rotation, targetRotation, _enemyAI.RotationSpeed*Time.deltaTime);

        

        if (!_enemyAI.PlayerDetected)
            _enemyAI.SwitchState(_enemyAI.RoamingState);
    }
    public override void ExitState()
    {
        
    }
}
