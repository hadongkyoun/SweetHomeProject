using UnityEngine;

public class PickUpTest : MonoBehaviour, IInteractable
{
    private bool canInteractWithPlayer;


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
        canInteractWithPlayer = isOn;
    }

}
