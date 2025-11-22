
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IInteractable
{
    public void Interact();
    public void Detected(bool isOn);
}


public class Interactor : MonoBehaviour
{

    [Header("Interact Parameters")]
    [SerializeField]
    private float detectCanInteractRange;
    [SerializeField]
    private float interactRangeMin;
    [SerializeField]
    private float interactRangeMax;
    // 플레이어가 오브젝트를 감지 했는지에 관한 변수
    private bool detectObject = false;
    // 감지된 오브젝트를 참조하는 변수
    private IInteractable detectedObject = null;
    [SerializeField]
    private LayerMask interactableMask;
    [SerializeField]
    private Transform interactorSource;

    [Space(15)]
    [Header("Player Cursor")]
    [SerializeField]
    private Image interactCursor;

    private Collider[] hitColliders = new Collider[5];
    private HashSet<Collider> trackColliders = new HashSet<Collider>();
    private List<Collider> newColliders = new List<Collider>();
    private HashSet<Collider> exitColliders = new HashSet<Collider>();

    private Vector3 characterCenter;

    private float angleMultiplier = 0.03271f;
    private float baseInteractRange = 0.0098f;
    private void Start()
    {
        if (TryGetComponent<CharacterController>(out CharacterController charController))
        {
            characterCenter = charController.center;
        }
    }


    private void FixedUpdate()
    {

        if (interactorSource == null)
        {
            Debug.LogError("Not Assgin interactor source");
            return;
        }

        FindCanInteractObject();

        PlayerInteract();
    }

    private void FindCanInteractObject()
    {
        int detectColliderNums = Physics.OverlapSphereNonAlloc(transform.position + characterCenter, detectCanInteractRange, hitColliders, interactableMask);
        if (detectColliderNums > 0)
        {
            Debug.Log("Detected");
            newColliders.Clear();
            // 실질적인 감지 된 Collider 들
            for (int i = 0; i < detectColliderNums; i++)
            {
                newColliders.Add(hitColliders[i]);
                // 추적하는 Collider HashSet ( 해당  Collider를 갖고 있지 않다면 Add )
                if (trackColliders.Add(hitColliders[i]))
                {
                    if (hitColliders[i].TryGetComponent<IInteractable>(out IInteractable interactable))
                    {
                        interactable.Detected(true);
                    }
                }
            }

            exitColliders.Clear();
            exitColliders.UnionWith(trackColliders);
            exitColliders.ExceptWith(newColliders);

            foreach (Collider collider in exitColliders)
            {
                if (collider.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.Detected(false);
                }
                trackColliders.Remove(collider);
            }
        }
        else
        {

            foreach (Collider collider in trackColliders)
            {
                if (collider.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.Detected(false);
                }
            }
            trackColliders.Clear();
        }
    }

    private void PlayerInteract()
    {
        //Debug.Log(interactorSource.eulerAngles.x);
        float xRotation = interactorSource.eulerAngles.x;

        if (xRotation > 180f)
            xRotation = 360f - xRotation;


        float finalRange = Mathf.Clamp(baseInteractRange + (xRotation * angleMultiplier), interactRangeMin, interactRangeMax);

        //Debug.Log(finalRange);

        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, finalRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                IsDetectObject(true, interactObj);
            }
            else
            {
                IsDetectObject(false, null);
            }
        }
        else
        {
            IsDetectObject(false, null);
        }
    }

    // 플레이어가 오브젝트를 감지 했는가
    private void IsDetectObject(bool isDetect, IInteractable interactObject)
    {
        detectObject = isDetect;
        detectedObject = interactObject;


        if (isDetect)
        {
            interactCursor.color = Color.yellow;
        }
        else
        {
            interactCursor.color = Color.white;
        }
    }

    public void TryInteract()
    {
        if (detectObject && detectedObject != null)
        {
            detectedObject.Interact();
        }
    }

    #region Gizmos
    //private void OnDrawGizmosSelected()
    //{
    //    // 1. 기즈모 색상 설정 (예: 밝은 녹색)
    //    Gizmos.color = Color.green;

    //    float radius = detectCanInteractRange;

    //    // 3. 구체의 와이어프레임(Wireframe)을 그립니다.
    //    // 이는 구체의 경계를 투명하게 보여줍니다.
    //    Gizmos.DrawWireSphere(transform.position + characterCenter, radius);


    //    // (선택 사항) 구체 내부를 반투명하게 채워서 더 잘 보이게 할 수 있습니다.
    //    // Gizmos.color = new Color(0, 1, 0, 0.2f); // 반투명 녹색
    //    // Gizmos.DrawSphere(position, radius);
    //}
    #endregion
}
