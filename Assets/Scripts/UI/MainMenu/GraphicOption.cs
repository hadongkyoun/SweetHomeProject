using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicOption : MonoBehaviour
{

    [SerializeField]
    private Volume globalVolume;

    [Space(15)]
    [Header("Brightness Setting")]
    [SerializeField]
    private Slider brightnessSlider;
    [SerializeField]
    private TextMeshProUGUI brightnessAmount;
    private float firstBrightnessAmount;


    [Header("Fullscreen Setting")]
    [SerializeField]
    private Toggle fullscreenToggle;
    private bool firstFullscreenToggled;

    [Header("Vsync Setting")]
    [SerializeField]
    private Toggle vsyncToggle;
    private bool firstVsyncToggled;

    [Header("Resolution Setting")]
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private int firstResolutionIndex;

    [Header("Quality Setting")]
    [SerializeField]
    private TMP_Dropdown qualityDropdown;
    private int firstQualityLevel;
    // 가장 최초의 Quality Level
    private int baseQualityLevel;

    [Header("Setting Handle")]
    [SerializeField]
    private Button BackBtn;
    [SerializeField]
    private Button ApplyBtn;
    private bool applyTrigger;
    [SerializeField]
    private Button ResetBtn;

    private OptionHandler optionHandler;

    private void Awake()
    {
        optionHandler = GetComponentInParent<OptionHandler>();


        baseQualityLevel = QualitySettings.GetQualityLevel();
        qualityDropdown.value = baseQualityLevel;
        qualityDropdown.RefreshShownValue();

        
        resolutionDropdown.ClearOptions();


        BackBtn.onClick.AddListener(ExitSoundSetting);
        ApplyBtn.onClick.AddListener(ApplyGameplaySetting);
        ResetBtn.onClick.AddListener(ResetGraphicSetting);

        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        vsyncToggle.onValueChanged.AddListener(SetVsync);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }
    private void Start()
    {

        List<string> options = new List<string>();

        for (int i = 0; i < GraphicManager.Instance.uniqueList.Count; i++)
        {
            Resolution res = GraphicManager.Instance.uniqueList[i];
            string option = $"{res.width} x {res.height}";
            options.Add(option);

        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = GraphicManager.Instance.BaseResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Initially disable Apply Button
        ApplyBtn.interactable = false;
    }
    void OnEnable()
    {
        InitSoundOption();
    }

    private void InitSoundOption()
    {
        applyTrigger = false;
        // Initially disable Apply Button
        ApplyBtn.interactable = false;

        // Brightness
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness", 1.0f);
        firstBrightnessAmount = brightnessSlider.value;

        // Fullscreen
        fullscreenToggle.isOn = ToggleIntSwitch(PlayerPrefs.GetInt("fullscreen", 1));
        firstFullscreenToggled = fullscreenToggle.isOn;

        // Vsync
        vsyncToggle.isOn = ToggleIntSwitch(PlayerPrefs.GetInt("vsync", 1));
        firstVsyncToggled = vsyncToggle.isOn;


        // Resolution
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", GraphicManager.Instance.BaseResolutionIndex);
        resolutionDropdown.RefreshShownValue();
        firstResolutionIndex = GraphicManager.Instance.BaseResolutionIndex;

        // Quality
        qualityDropdown.value = PlayerPrefs.GetInt("quality", baseQualityLevel);
        qualityDropdown.RefreshShownValue();
        firstQualityLevel = qualityDropdown.value;

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

    #region Brightness
    public void SetBrightness(float amount)
    {
        CheckApplyState();

        brightnessAmount.text = $"{amount:F1}";
    }
    #endregion

    #region Fullscreen
    private void SetFullscreen(bool isOn)
    {
        CheckApplyState();
    }
    #endregion

    #region Vsync
    private void SetVsync(bool isOn)
    {
        CheckApplyState();
    }
    #endregion

    #region Resolution
    private void SetResolution(Int32 value)
    {
        CheckApplyState();
    }
    #endregion

    #region Quality
    private void SetQuality(Int32 value)
    {
        CheckApplyState();
    }
    #endregion


    #region Apply
    private void ApplyGameplaySetting()
    {
        // Brightness
        if (brightnessSlider.value != firstBrightnessAmount)
        {
            PlayerPrefs.SetFloat("brightness", brightnessSlider.value);
            firstBrightnessAmount = brightnessSlider.value;
        }

        if (fullscreenToggle.isOn != firstFullscreenToggled)
        {
            PlayerPrefs.SetInt("fullscreen", ToggleIntSwitch(fullscreenToggle.isOn));
            firstFullscreenToggled = fullscreenToggle.isOn;
        }

        if (vsyncToggle.isOn != firstVsyncToggled)
        {
            PlayerPrefs.SetInt("vsync", ToggleIntSwitch(vsyncToggle.isOn));
            firstVsyncToggled = vsyncToggle.isOn;
        }

        if (resolutionDropdown.value != firstResolutionIndex)
        {
            PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
            firstResolutionIndex = resolutionDropdown.value;
        }

        if (qualityDropdown.value != firstQualityLevel)
        {
            PlayerPrefs.SetInt("quality", qualityDropdown.value);
            firstQualityLevel = qualityDropdown.value;
        }

        GraphicManager.Instance.ApplyGraphicSet(brightnessSlider.value, fullscreenToggle.isOn, ToggleIntSwitch(vsyncToggle.isOn), resolutionDropdown.value, qualityDropdown.value);

        applyTrigger = true;

        ApplyBtn.interactable = false;
    }
    private void CheckApplyState()
    {
        bool isChanged = false;

        // 하나라도 다르면 true
        if (brightnessSlider.value != firstBrightnessAmount) isChanged = true;
        else if (fullscreenToggle.isOn != firstFullscreenToggled) isChanged = true;
        else if (qualityDropdown.value != firstQualityLevel) isChanged = true;
        else if (vsyncToggle.isOn != firstVsyncToggled) isChanged = true;
        else if (resolutionDropdown.value != firstResolutionIndex) isChanged = true;

        ApplyBtn.interactable = isChanged;
    }

    #endregion

    #region Reset
    // Reset
    private void ResetGraphicSetting()
    {

        brightnessSlider.value = 1f;

        fullscreenToggle.isOn = true;

        vsyncToggle.isOn = false;

        resolutionDropdown.value = GraphicManager.Instance.BaseResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        qualityDropdown.value = baseQualityLevel;
        qualityDropdown.RefreshShownValue();



    }
    #endregion


    #region Exit
    // Exit
    private void ExitSoundSetting()
    {

        optionHandler.CloseFilm();
        gameObject.SetActive(false);
    }
    #endregion

}
