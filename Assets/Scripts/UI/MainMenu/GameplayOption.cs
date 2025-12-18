using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    private Toggle headbobToggl;
    private bool firstHeadbobToggled;

    [Header("Setting Handle")]
    [SerializeField]
    private Button BackBtn;
    [SerializeField]
    private Button ApplyBtn;
    private bool applyTrigger;
    [SerializeField]
    private Button ResetBtn;
    [SerializeField]
    private GameObject OptionFilm;

    void Awake()
    {
        BackBtn.onClick.AddListener(ExitSoundSetting);
        ApplyBtn.onClick.AddListener(ApplySoundSetting);
        ResetBtn.onClick.AddListener(ResetSoundSetting);

        sensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        invertYToggle.onValueChanged.AddListener(SetInvertY);
    }

    void OnEnable()
    {
        InitSoundOption();
    }

    private void InitSoundOption()
    {
        OptionFilm.SetActive(true);
        applyTrigger = false;

        // Mouse Sensitivity
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 0.2f);
        firstSensitivityAmount = sensitivitySlider.value;

        // Invert Y
        invertYToggle.isOn = ToggleIntSwitch(PlayerPrefs.GetInt("invertY", 0));
        firstInvertYToggled = invertYToggle.isOn;

        //// Background Volume
        //bgVSlider.value = PlayerPrefs.GetFloat("backgroundVolume", 0.5f);
        //firstBgVolume = bgVSlider.value;

        //// Effect Volume
        //effectVSlider.value = PlayerPrefs.GetFloat("effectVolume", 0.5f);
        //firstEffectVolume = effectVSlider.value;

        //// Voice Volume
        //voiceVSlider.value = PlayerPrefs.GetFloat("voiceVolume", 0.5f);
        //firstVoiceVolume = voiceVSlider.value;

        // Initially disable Apply Button
        ApplyBtn.interactable = false;
    }


    #region Mouse Sensitivity
    public void SetMouseSensitivity(float amount)
    {
        CheckApplyState();

        sensitivityAmount.text = $"{amount * 10}";
    }
    #endregion

    #region Invert Y
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

    private void SetInvertY(bool isOn)
    {
        CheckApplyState();
    }
    #endregion

    #region Apply
    private void ApplySoundSetting()
    {
        // Mouse Sensitivity
        if (sensitivitySlider.value != firstSensitivityAmount)
        {
            PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
        }

        if(invertYToggle.isOn != firstInvertYToggled)
        {
            PlayerPrefs.SetInt("invertY", ToggleIntSwitch(invertYToggle.isOn));
        }

        ////Background Volume
        //if (bgVSlider.value != firstBgVolume)
        //{
        //    PlayerPrefs.SetFloat("backgroundVolume", bgVSlider.value);
        //}

        ////Effect Volume
        //if (effectVSlider.value != firstEffectVolume)
        //{
        //    PlayerPrefs.SetFloat("effectVolume", effectVSlider.value);
        //}

        ////Voice Volume
        //if (voiceVSlider.value != firstVoiceVolume)
        //{
        //    PlayerPrefs.SetFloat("voiceVolume", voiceVSlider.value);
        //}
        applyTrigger = true;

        ApplyBtn.interactable = false;
    }
    private void CheckApplyState()
    {
        bool isChanged = false;

        // 하나라도 다르면 true
        if (sensitivitySlider.value != firstSensitivityAmount) isChanged = true;
        else if (invertYToggle.isOn != firstInvertYToggled) isChanged = true;
        //else if (effectVSlider.value != firstEffectVolume) isChanged = true;
        //else if (voiceVSlider.value != firstVoiceVolume) isChanged = true;

        ApplyBtn.interactable = isChanged;
    }
    #endregion

    #region Reset
    // Reset
    private void ResetSoundSetting()
    {

        // Master Volume
        sensitivitySlider.value = 0.2f;

        invertYToggle.isOn = false;

        //// Background Volume
        //bgVSlider.value = 0.5f;

        //// Effect Volume
        //effectVSlider.value = 0.5f;

        //// Voice Volume
        //voiceVSlider.value = 0.5f;

        //SetMasterVolume(0.5f);
        //SetBackgroundVolume(0.5f);
        //SetEffectVolume(0.5f);
        //SetVoiceVolume(0.5f);
    }
    #endregion


    #region Exit
    // Exit
    private void ExitSoundSetting()
    {
        if (!applyTrigger)
        {
            SetMouseSensitivity(firstSensitivityAmount);
            SetInvertY(firstInvertYToggled);
            //SetEffectVolume(firstEffectVolume);
            //SetVoiceVolume(firstVoiceVolume);
        }

        OptionFilm.SetActive(false);
        gameObject.SetActive(false);
    }
    #endregion
}
