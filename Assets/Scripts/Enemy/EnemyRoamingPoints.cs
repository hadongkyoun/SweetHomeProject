using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamingPoints : MonoBehaviour
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

    public Transform GetRoamingPoint()
    {
        return childTransforms[Random.Range(0, childTransforms.Count)]; 
    }
}
