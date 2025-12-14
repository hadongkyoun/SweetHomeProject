using UnityEngine;

public class InvestigateHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject investigatePanel;

    private CanvasGroup investigateFlim;

    private GameObject itemPrefab;

    [SerializeField]
    private InvestigateMode investigateMode;

    private void Start()
    {
        investigateFlim = GetComponent<CanvasGroup>();
    }

    public void InvestigateModeOn(Item item, bool isOn)
    {
        investigatePanel.SetActive(isOn);
        FilmOn(isOn);

        if (!isOn)
            return;

        if (itemPrefab != null)
        {
            Destroy(itemPrefab);
        }


        // 첫 생성시 Interact UI 해제
        itemPrefab = Instantiate(item.GetItemPrefab(), new Vector3(1000, 1000, 1000), Quaternion.identity);
        itemPrefab.GetComponentInChildren<InteractUI>().UnnecessaryInteractUI();


        investigateMode.SetItemPrefabForInvestigate(itemPrefab);
    }

    public void ResetEvent()
    {
        if (itemPrefab != null)
        {
            Destroy(itemPrefab.gameObject);
        }
        investigatePanel.SetActive(false);
    }


    public bool IsInvestigateModeOn()
    {
        return investigatePanel.activeSelf;
    }

    private void FilmOn(bool isOn)
    {
        if (isOn)
        {
            investigateFlim.blocksRaycasts = true;
            investigateFlim.interactable = true;
        }
        else
        {
            investigateFlim.blocksRaycasts = false;
            investigateFlim.interactable = false;
        }
    }
}
