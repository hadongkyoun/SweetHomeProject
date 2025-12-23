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
    private int baseResolutionIndex;

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

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        baseResolutionIndex = 0;


        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                baseResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = baseResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        BackBtn.onClick.AddListener(ExitSoundSetting);
        ApplyBtn.onClick.AddListener(ApplyGameplaySetting);
        ResetBtn.onClick.AddListener(ResetGraphicSetting);

        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        vsyncToggle.onValueChanged.AddListener(SetVsync);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        qualityDropdown.onValueChanged.AddListener(SetQuality);


        if (brightnessSlider.value != firstBrightnessAmount)
        {
            // Set Brightness
            if (globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.postExposure.value = brightnessSlider.value;
            }
        }


        ////Fullscreen
        //Screen.fullScreen = ToggleIntSwitch(PlayerPrefs.GetInt("fullscreen", 0));

        ////Vsync
        //QualitySettings.vSyncCount = PlayerPrefs.GetInt("vsync", 1);

        //if (QualitySettings.vSyncCount == 0)
        //    Application.targetFrameRate = 60;

        //Resolution resolution = resolutions[PlayerPrefs.GetInt("resolution", baseResolutionIndex)];
        //Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);



        ////Quality
        //if (qualityDropdown.value != firstQualityLevel)
        //{
        //    // Set Quality
        //    QualitySettings.SetQualityLevel(qualityDropdown.value);
        //}
    }
    void OnEnable()
    {
        InitSoundOption();
    }

    private void InitSoundOption()
    {
        applyTrigger = false;

        // Brightness
        brightnessSlider.value = PlayerPrefs.GetFloat("brightness", 1.0f);
        firstBrightnessAmount = brightnessSlider.value;

        // Fullscreen
        fullscreenToggle.isOn = ToggleIntSwitch(PlayerPrefs.GetInt("fullscreen", 0));
        firstFullscreenToggled = fullscreenToggle.isOn;

        // Vsync
        vsyncToggle.isOn = ToggleIntSwitch(PlayerPrefs.GetInt("vsync", 1));
        firstVsyncToggled = vsyncToggle.isOn;

        // Resolution
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", baseResolutionIndex);
        resolutionDropdown.RefreshShownValue();
        firstResolutionIndex = baseResolutionIndex;

        // Quality
        qualityDropdown.value = PlayerPrefs.GetInt("quality", baseQualityLevel);
        qualityDropdown.RefreshShownValue();
        firstQualityLevel = qualityDropdown.value;

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
        }

        if (fullscreenToggle.isOn != firstFullscreenToggled)
        {
            PlayerPrefs.SetInt("fullscreen", ToggleIntSwitch(fullscreenToggle.isOn));
        }

        if (vsyncToggle.isOn != firstVsyncToggled)
        {
            PlayerPrefs.SetInt("vsync", ToggleIntSwitch(vsyncToggle.isOn));
        }

        if (resolutionDropdown.value != firstResolutionIndex)
        {
            PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
        }

        if (qualityDropdown.value != firstQualityLevel)
        {
            PlayerPrefs.SetInt("quality", qualityDropdown.value);
        }

        ApplyGraphicSet();

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

    private void ApplyGraphicSet()
    {
        //Brightness
        if (brightnessSlider.value != firstBrightnessAmount)
        {
            //// Set Brightness
            //if (globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
            //{
            //    colorAdjustments.postExposure.value = brightnessSlider.value;
            //}
        }
        //Fullscreen
        if (fullscreenToggle.isOn != firstFullscreenToggled)
        {
            // Set Fullscreen
            Screen.fullScreen = fullscreenToggle.isOn;
        }

        //Vsync
        if (vsyncToggle.isOn != firstVsyncToggled)
        {
            QualitySettings.vSyncCount = ToggleIntSwitch(vsyncToggle.isOn);

            if (QualitySettings.vSyncCount == 0)
                Application.targetFrameRate = 60;
        }

        //Resolution
        if (resolutionDropdown.value != firstResolutionIndex)
        {
            Resolution resolution = resolutions[resolutionDropdown.value];
            Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);

        }

        //Quality
        if (qualityDropdown.value != firstQualityLevel)
        {
            // Set Quality
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        }
    }
    #endregion

    #region Reset
    // Reset
    private void ResetGraphicSetting()
    {

        brightnessSlider.value = 1f;

        fullscreenToggle.isOn = false;

        resolutionDropdown.value = baseResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        qualityDropdown.value = baseQualityLevel;
        qualityDropdown.RefreshShownValue();
        vsyncToggle.isOn = false;

    }
    #endregion


    #region Exit
    // Exit
    private void ExitSoundSetting()
    {
        if (!applyTrigger)
        {
            SetBrightness(firstBrightnessAmount);
            SetFullscreen(firstFullscreenToggled);
            SetVsync(firstVsyncToggled);
            SetResolution(firstResolutionIndex);
            SetQuality(firstQualityLevel);
        }

        optionHandler.CloseFilm();
        gameObject.SetActive(false);
    }
    #endregion
}
