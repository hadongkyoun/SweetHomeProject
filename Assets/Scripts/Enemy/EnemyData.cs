using UnityEngine;
using UnityEngine.AI;

public class EnemyData : MonoBehaviour
{
    private NavMeshAgent navAgent;

    [Header("Enemy Roaming")]
    [SerializeField]
    private float maxStayTime;
    [SerializeField]
    private float minStayTime;


    [Space(15)]
    [Header("Enemy Move : It have to be same with Animator ThresHold!!!")]
    [SerializeField]
    private float roamingSpeed;
    public float RoamingSpeed { get { return roamingSpeed; } }
    [SerializeField]
    private float trackingSpeed;
    public float TrackingSpeed { get { return trackingSpeed; } }
    [SerializeField]
    private float rotationSpeed;
    public float RotationSpeed
    {
        get { return rotationSpeed; }
    }


    [Space(15)]
    [Header("Enemy Vision")]
    [SerializeField]
    private float detectRange = 10;
    public float DetectRange { get { return detectRange; } }
    [SerializeField]
    private float detectAngle = 45;
    public float DetectAngle { get { return detectAngle; } }



    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        navAgent.speed = trackingSpeed;
    }

    public float GetRandomStayTime()
    {
        return Random.Range(minStayTime,maxStayTime);
    }

}
