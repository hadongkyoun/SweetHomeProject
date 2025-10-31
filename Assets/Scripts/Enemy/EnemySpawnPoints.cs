using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    private List<Transform> childTransforms = new List<Transform>();
    void Start()
    {
        childTransforms.Clear();
        foreach (Transform child in transform)
        {
            childTransforms.Add(child);
        }
    }

    public Transform GetSpawnPoint()
    {
        return childTransforms[Random.Range(0, childTransforms.Count)];
    }
}
