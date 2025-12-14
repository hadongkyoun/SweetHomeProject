using UnityEngine;

public class InvestigateMode : MonoBehaviour
{

    private InvestigateHandler investigateHandler;

    private Transform itemPrefabTransform;


    private void Awake()
    {
        investigateHandler = GetComponentInParent<InvestigateHandler>();
    }

    void Update()
    {
        if (!gameObject.activeSelf || itemPrefabTransform == null)
            return;

        if (Input.GetMouseButton(0))
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
    }

    private void FinishInvestigate()
    {
        if(itemPrefabTransform != null)
            Destroy(itemPrefabTransform.gameObject);

        investigateHandler.InvestigateModeOn(null, false);
    }
}
