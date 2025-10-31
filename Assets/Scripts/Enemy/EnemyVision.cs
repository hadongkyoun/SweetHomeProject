using TreeEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class EnemyVision : MonoBehaviour
{
    [SerializeField]
    private EnemyAI enemyAI;
    public EnemyAI EnemyAI { set { enemyAI = value; } }

    private GameObject player;
    public GameObject GetPlayer { get { return player; } }
    private float playerHeight;

    private float detectRange;
    private float detectAngle;

    RaycastHit hit;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHeight = player.GetComponent<CharacterController>().height;
    }

    bool isInAngle, isInRange, isNotHidden;

    private void Update()
    {
        if(enemyAI == null)
        {
            Debug.LogError("ERROR!! : Need to link enemyAI from enemyVision");
        }


        if(Vector3.Distance(transform.position, player.transform.position) < detectRange)
        {
            isInRange = true;
            Debug.Log("Enemy : Player is in range");
        }
        else
        {
            isInRange = false;
        }



        // 각도 확인


        
        if (Physics.Raycast(transform.position, (new Vector3(player.transform.position.x, player.transform.position.y + playerHeight/2f, player.transform.position.z) - transform.position), 
                                                        out hit, Mathf.Infinity))
        {
            if(hit.transform.CompareTag("Player"))
            {
                isNotHidden = true;
                Debug.Log("Enemy : Player is not hidden");
            }
            else
            {
                isNotHidden = false;
            }
        }
        else
        {
            isNotHidden = false;
        }


        Vector3 side1 = new Vector3(player.transform.position.x, player.transform.position.y + playerHeight / 2f, player.transform.position.z) - transform.position;
        Vector3 side2 = transform.forward;

        float angle = Vector3.SignedAngle(side1, side2, transform.up);

        if (angle < detectAngle && angle > -detectAngle)
        {
            isInAngle = true;
        }
        else
        {
            isInAngle = false;
        }

        if(isInRange && isInAngle && isNotHidden)
        {
            enemyAI.PlayerDetected = true;
            Debug.Log("Detected");
        }
        

        if(enemyAI.PlayerDetected)
        {
            // 멀리 떨어져 있는 상태에서
            if(!isInRange)
            {
                // 숨었을때
                if (!isNotHidden)
                {
                    enemyAI.PlayerDetected = false;
                    
                }
            }
        }
    }

    public void SetValue(EnemyData enemyData)
    {
        detectRange = enemyData.DetectRange;
        detectAngle = enemyData.DetectAngle;
    }

    

}
