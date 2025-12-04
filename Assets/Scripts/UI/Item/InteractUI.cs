using UnityEngine;

public class InteractUI : MonoBehaviour
{


    private GameObject uiObject;
    private Camera mainCamera;

    private bool activated = false;

    public void Initialize(GameObject interactableUI)
    {
        this.uiObject = interactableUI;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }



    private void LateUpdate()
    {
        if (!activated)
            return;
        uiObject.transform.LookAt(uiObject.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }


    public void ActivateUI(bool isOn)
    {
        if (activated == isOn)
            return;

        activated = isOn;
        uiObject.transform.gameObject.SetActive(activated);
    }

}
