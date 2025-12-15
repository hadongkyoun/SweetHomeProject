using UnityEngine;
using UnityEngine.EventSystems;

public class InvestigateMode : MonoBehaviour, IDragHandler
{

    private InvestigateHandler investigateHandler;

    private Transform itemPrefabTransform;

    [Header("Investigate Mode Setting")]
    [SerializeField]
    private GameObject investigateCamPrefab;
    private Transform investigateCamrea;
    [SerializeField]
    private float sensitivity = 0.2f;
    [SerializeField]
    private float smoothTime = 15f;

    private Quaternion targetRotation;

    private bool investigateSettingComplete = false;

    private void Awake()
    {
        investigateHandler = GetComponentInParent<InvestigateHandler>();
        investigateCamrea = Instantiate(investigateCamPrefab, null).transform;
        
    }

    void Update()
    {
        if (!gameObject.activeSelf || itemPrefabTransform == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Cursor.visible = true;
        }
        else if(Input.GetMouseButton(0))
        {

        }

        if (Input.GetMouseButtonDown(1))
        {
            FinishInvestigate();
        }

    }

    public void SetItemPrefabForInvestigate(GameObject itemPrefab)
    {
        itemPrefabTransform = itemPrefab.transform;
        targetRotation = itemPrefabTransform.rotation;

        investigateSettingComplete = true;
    }

    private void FinishInvestigate()
    {
        if(itemPrefabTransform != null)
            Destroy(itemPrefabTransform.gameObject);

        investigateHandler.InvestigateModeOn(null, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(investigateSettingComplete == false)
        {
            return;
        }

        // 1. 입력 감도 적용 (Time.deltaTime 곱하지 않음)
        float xInput = eventData.delta.x * sensitivity;
        float yInput = eventData.delta.y * sensitivity;

        // 2. 회전 축 설정 (사용자님이 원하신 investigateTransform.up 등 활용)
        // 가로 드래그(xInput) -> 물체를 기준으로 Up/Down 축 회전? 혹은 카메라 기준 Up?
        // 보통 '조사하기' 모드에서는 카메라의 Up/Right 축을 기준으로 돌리는 게 직관적입니다.

        Vector3 upAxis = investigateCamrea.up; 
        Vector3 rightAxis = investigateCamrea.right;

        // 3. 목표 회전값(_targetRotation)을 'Rotate' 시킴
        // 쿼터니언 곱셈은 transform.Rotate()와 같은 효과를 냅니다.

        // 좌우 드래그 -> Y축 회전 (보통 -xInput을 해야 마우스 따라옴)
        Quaternion yRotation = Quaternion.AngleAxis(-xInput, upAxis);

        // 상하 드래그 -> X축 회전
        Quaternion xRotation = Quaternion.AngleAxis(yInput, rightAxis);

        // 기존 목표값에 새로운 회전을 적용 (순서 중요: Global축 기준이면 앞에 곱함)
        targetRotation = yRotation * xRotation * targetRotation;
    }

    void LateUpdate()
    {
        if (investigateSettingComplete == false)
        {
            return;
        }

        itemPrefabTransform.rotation = Quaternion.Slerp(itemPrefabTransform.rotation, targetRotation, smoothTime * Time.deltaTime);
    }

}
