using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayOption : MonoBehaviour
{

    [Header("Mouse Sensitivity Setting")]
    [SerializeField]
    private Slider sensitivitySlider;
    [SerializeField]
    private TextMeshProUGUI sensitivityAmount;
    private float firstSensitivityAmount;

    [Header("Invert Y Setting")]
    [SerializeField]
    private Toggle invertYToggle;
    private bool firstInvertYToggled;

    [Header("Headbob Setting")]
    [SerializeField]
    private Toggle headbobToggle;
    private bool firstHeadbobToggled;

    [Header("FOV Setting")]
    [SerializeField]
    private Slider fovSlider;
    [SerializeField]
    private TextMeshProUGUI fovAmount;
    private float firstFOVAmount;

    [Header("Setting Handle")]
    [SerializeField]
    private Button BackBtn;
    [SerializeField]
    private Button ApplyBtn;
    private bool applyTrigger;
    [SerializeField]
    private Button ResetBtn;

    private OptionHandler optionHandler;

    void Awake()
    {
        optionHandler = GetComponentInParent<OptionHandler>();

        BackBtn.onClick.AddListener(ExitGameplaySetting);
        ApplyBtn.onClick.AddListener(ApplyGameplaySetting);
        ResetBtn.onClick.AddListener(ResetGameplaySetting);

        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        invertYToggle.onValueChanged.AddListener(SetInvertY);
        headbobToggle.onValueChanged.AddListener(SetHeadbob);
        fovSlider.onValueChanged.AddListener(SetFOVSensitivity);
    }

    void OnEnable()
    {
        InitSoundOption();
    }

    private void InitSoundOption()
    {
        applyTrigger = false;

        // Mouse Sensitivity
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 0.2f);
        firstSensitivityAmount = sensitivitySlider.value;

        // Invert Y
        invertYToggle.isOn = ToggleIntSwitch(PlayerPrefs.GetInt("invertY", 0));
        firstInvertYToggled = invertYToggle.isOn;

        // Headbob
        headbobToggle.isOn = ToggleIntSwitch(PlayerPrefs.GetInt("headbob", 0));
        firstHeadbobToggled = headbobToggle.isOn;

        // FOV
        fovSlider.value = PlayerPrefs.GetFloat("fov", 60f);
        firstFOVAmount = fovSlider.value;

        // Initially disable Apply Button
        ApplyBtn.interactable = false;
    }

    #region Toggle Method
    private bool ToggleIntSwitch(int isOn)
    {
        if (isOn == 1)
            return true;
        else
            return false;
    }
    private int ToggleIntSwitch(bool isOn)
    {
        if (isOn)
            return 1;
        else
            return 0;
    }
    #endregion

    #region Mouse Sensitivity
    public void SetMouseSensitivity(float amount)
    {
        CheckApplyState();

        sensitivityAmount.text = $"{amount * 10:F1}";
    }
    #endregion


    #region Invert Y
    private void SetInvertY(bool isOn)
    {
        CheckApplyState();
    }
    #endregion

    #region Headbob
    private void SetHeadbob(bool isOn)
    {
        CheckApplyState();
    }
    #endregion

    #region FOV
    public void SetFOVSensitivity(float amount)
    {
        CheckApplyState();

        fovAmount.text = $"{amount:F0}";
    }
    #endregion

    #region Apply
    private void ApplyGameplaySetting()
    {
        // Mouse Sensitivity
        if (sensitivitySlider.value != firstSensitivityAmount)
        {
            PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
            firstSensitivityAmount = sensitivitySlider.value;
        }

        if (invertYToggle.isOn != firstInvertYToggled)
        {
            PlayerPrefs.SetInt("invertY", ToggleIntSwitch(invertYToggle.isOn));
            firstInvertYToggled = invertYToggle.isOn;
        }

        if (headbobToggle.isOn != firstHeadbobToggled)
        {
            PlayerPrefs.SetInt("headbob", ToggleIntSwitch(headbobToggle.isOn));
            firstHeadbobToggled = headbobToggle.isOn;
        }

        if (fovSlider.value != firstFOVAmount)
        {
            PlayerPrefs.SetFloat("fov", fovSlider.value);
            firstFOVAmount = fovSlider.value;
        }

        applyTrigger = true;

        ApplyBtn.interactable = false;

        GameManager.Instance.UpdateGameSetting("Gameplay");
    }
    private void CheckApplyState()
    {
        bool isChanged = false;

        // 하나라도 다르면 true
        if (sensitivitySlider.value != firstSensitivityAmount) isChanged = true;
        else if (invertYToggle.isOn != firstInvertYToggled) isChanged = true;
        else if (headbobToggle.isOn != firstHeadbobToggled) isChanged = true;
        else if (fovSlider.value != firstFOVAmount) isChanged = true;

        ApplyBtn.interactable = isChanged;
    }
    #endregion

    #region Reset
    // Reset
    private void ResetGameplaySetting()
    {

        sensitivitySlider.value = 0.2f;

        invertYToggle.isOn = false;

        headbobToggle.isOn = false;

        fovSlider.value = 60f;

    }
    #endregion


    #region Exit
    // Exit
    private void ExitGameplaySetting()
    {
        optionHandler.CloseFilm();
        gameObject.SetActive(false);
    }
    #endregion
}
