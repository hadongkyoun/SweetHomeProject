using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState 
{
    public abstract void EnterState(EnemyAI enemyAI, EnemyData enemyData);

    public abstract void UpdateState();

    public abstract void ExitState();
}
