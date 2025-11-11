using UnityEngine;

public class NumberGenerator : MonoBehaviour, IInteractable
{
    private bool canInteractWithPlayer;
    public void Detected(bool isOn)
    {
        canInteractWithPlayer = isOn;
    }

    public void Interact()
    {
        if(canInteractWithPlayer == false)
        {
            return;
        }

        Debug.Log(Random.Range(0, 100));
    }
}
