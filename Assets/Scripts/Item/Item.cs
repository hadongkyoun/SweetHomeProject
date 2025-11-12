using UnityEngine;


[RequireComponent(typeof(InteractUI))]
public class Item : MonoBehaviour, IInteractable
{
    private bool canInteractWithPlayer;
    private InteractUI interactUI;

    private void Awake()
    {
        interactUI = GetComponent<InteractUI>();   
    }

    public void Interact()
    {
        if (!canInteractWithPlayer)
        {
            return;
        }

        Debug.Log($"Picked up :{gameObject.name}");
    }

    public void Detected(bool isOn)
    {
        if (canInteractWithPlayer == isOn)
            return;
        interactUI.ActivateUI(isOn);

        canInteractWithPlayer = isOn;
        
    }

}
