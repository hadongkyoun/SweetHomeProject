using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField]
    private Transform uiObjectTransform;
    private Camera mainCamera;

    private bool activated = false;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (!activated)
            return;
        uiObjectTransform.LookAt(uiObjectTransform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    public void ActivateUI(bool isOn)
    {
        if (activated == isOn)
            return;

        activated = isOn;
        uiObjectTransform.gameObject.SetActive(activated);
    }
}
