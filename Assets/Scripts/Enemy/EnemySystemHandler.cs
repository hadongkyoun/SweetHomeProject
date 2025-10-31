using System.Collections.Generic;
using UnityEngine;

public class EnemySystemHandler : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    // ���߿� �پ��� ������ ������ ���� Dictionary�� ���� �� ����
    private List<EnemyAI> enemys = new List<EnemyAI>();

    private EnemySpawnPoints enemySpawnPoints;
    private EnemyRoamingPoints enemyRoamingPoints;

    private void Awake()
    {
        enemySpawnPoints = GetComponentInChildren<EnemySpawnPoints>();
        enemyRoamingPoints = GetComponentInChildren<EnemyRoamingPoints>();
    }

    private void Start()
    {
        // ���� ����
        GameObject enemy = Instantiate(EnemyPrefabs[0]);
        enemy.transform.position = enemySpawnPoints.GetSpawnPoint().position;
        if(enemy.TryGetComponent<EnemyAI>(out EnemyAI enemyAI))
        {
            enemys.Add(enemyAI);
            enemyAI.SetEnemySystemHandler(this);
        }
    }

    public Transform GetEnemyRoamingPoint()
    {
        return enemyRoamingPoints.GetRoamingPoint();
    }
}
