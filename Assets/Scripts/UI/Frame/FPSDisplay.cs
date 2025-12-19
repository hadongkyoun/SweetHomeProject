using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI FpsText;

    [SerializeField]
    private float updateTime = 1f;
    private float currentTime;

    private int frameCount;
    private int frameRate;


    private void Start()
    {
        FpsText.text = $"{Application.targetFrameRate} FPS";
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        frameCount++;

        if (currentTime >= updateTime)
        {
            frameRate = Mathf.RoundToInt(frameCount / currentTime);
            FpsText.text = $"{frameRate} FPS";

            
            currentTime -= updateTime;
            frameCount = 0;
        }

    }
}
