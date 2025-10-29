using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
