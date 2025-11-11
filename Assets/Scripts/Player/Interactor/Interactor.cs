
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void Detected(bool isOn);
}


public class Interactor : MonoBehaviour
{

    [Space(15)]
    [Header("Interact Parameters")]
    [SerializeField]
    private float activeInteractRange;
    [SerializeField]
    private float canInteractRange;
    [SerializeField]
    private LayerMask interactableMask;
    [SerializeField]
    private Transform interactorSource;

    private Collider[] hitColliders = new Collider[5];
    private HashSet<Collider> trackColliders = new HashSet<Collider>();
    private List<Collider> newColliders = new List<Collider>();
    private HashSet<Collider> exitColliders = new HashSet<Collider>();

    private Vector3 characterCenter;
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
            return;

        int detectColliderNums = Physics.OverlapSphereNonAlloc(transform.position + characterCenter, activeInteractRange, hitColliders, interactableMask);
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




    public void TryInteract()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, canInteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 1. 기즈모 색상 설정 (예: 밝은 녹색)
        Gizmos.color = Color.green;

        float radius = activeInteractRange;

        // 3. 구체의 와이어프레임(Wireframe)을 그립니다.
        // 이는 구체의 경계를 투명하게 보여줍니다.
        Gizmos.DrawWireSphere(transform.position + characterCenter, radius);

        // (선택 사항) 구체 내부를 반투명하게 채워서 더 잘 보이게 할 수 있습니다.
        // Gizmos.color = new Color(0, 1, 0, 0.2f); // 반투명 녹색
        // Gizmos.DrawSphere(position, radius);
    }
}
