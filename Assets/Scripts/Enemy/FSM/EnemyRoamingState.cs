using UnityEngine;
using UnityEngine.AI;

public class EnemyRoamingState : EnemyBaseState
{
    private EnemyAI _enemyAI;
    private Transform _roamingPoint;

    private Vector3 _enemyDirection;

    private bool setRoamingPos;

    private float _currentTime;
    private float _stayTime;
    public override void EnterState(EnemyAI enemyAI)
    {
        if (_enemyAI == null)
        {
            _enemyAI = enemyAI;
        }

        _roamingPoint = _enemyAI.GetRoamingPoint();

        _currentTime = 0.0f;
        _stayTime = _enemyAI.GetStayTime();

        setRoamingPos = true;

    }

    public override void UpdateState()
    {
        //Debug.Log($"[Roaming State] Speed : {navMeshAgent.speed}");

        if (setRoamingPos)
        {
            NavMeshPath path = new NavMeshPath();
            _enemyAI.AgentAI.CalculatePath(_roamingPoint.position, path);
            _enemyAI.AgentAI.SetPath(path);
            path = null;

            setRoamingPos = false;
            _enemyAI.SetStateSpeed(_enemyAI.RoamingSpeed);
        }



        if (_enemyAI.AgentAI.remainingDistance <= _enemyAI.AgentAI.stoppingDistance)
        {
            _enemyAI.SetStateSpeed(0);

            _currentTime += Time.deltaTime;

            if(_currentTime >= _stayTime)
            {
                setRoamingPos = true;
                _roamingPoint = _enemyAI.GetRoamingPoint();
                _currentTime = 0.0f;
            }
        }
        else
        {
            _enemyDirection = _enemyAI.AgentAI.desiredVelocity;
        }


        Quaternion targetRotation = Quaternion.LookRotation(_enemyDirection);
        _enemyAI.transform.rotation = Quaternion.Lerp(_enemyAI.transform.rotation, targetRotation, _enemyAI.RotationSpeed * Time.deltaTime);

        if (_enemyAI.PlayerDetected)
            _enemyAI.SwitchState(_enemyAI.TrackingState);
    }
    public override void ExitState()
    {
    }
}
