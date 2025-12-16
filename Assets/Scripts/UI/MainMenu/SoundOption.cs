using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{

    [Header("Audio Setting")]
    [SerializeField]
    private AudioMixer audioMixer;


    [Space(15)]
    [Header("Master")]
    [SerializeField]
    private Slider masterVSlider;
    [SerializeField]
    private TextMeshProUGUI masterVGage;
    private float firstMasterVolume;

    [Header("BackGround")]
    [SerializeField]
    private Slider bgVSlider;
    [SerializeField]
    private TextMeshProUGUI bgVGage;
    private float firstBgVolume;

    [Header("Effect")]
    [SerializeField]
    private Slider effectVSlider;
    [SerializeField]
    private TextMeshProUGUI effectVGage;
    private float firstEffectVolume;

    [Header("Voice")]
    [SerializeField]
    private Slider voiceVSlider;
    [SerializeField]
    private TextMeshProUGUI voiceVGage;
    private float firstVoiceVolume;

    [Header("Setting Handle")]
    [SerializeField]
    private Button BackBtn;
    [SerializeField]
    private Button ApplyBtn;
    [SerializeField]
    private Button ResetBtn;


    private void Awake()
    {
        BackBtn.onClick.AddListener(ExitSoundSetting);
        ApplyBtn.onClick.AddListener(ApplySoundSetting);


        masterVSlider.onValueChanged.AddListener(SetMasterVolume);
        bgVSlider.onValueChanged.AddListener(SetBackgroundVolume);
        effectVSlider.onValueChanged.AddListener(SetEffectVolume);
        voiceVSlider.onValueChanged.AddListener(SetVoiceVolume);
    }

    private void Start()
    {
        InitSoundOption();
    }

    private void InitSoundOption()
    {

        // Master Volume
        masterVSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        firstMasterVolume = masterVSlider.value;

        // Background Volume
        bgVSlider.value = PlayerPrefs.GetFloat("backgroundVolume",0.5f);
        firstBgVolume = bgVSlider.value;

        // Effect Volume
        effectVSlider.value = PlayerPrefs.GetFloat("effectVolume", 0.5f);
        firstEffectVolume = effectVSlider.value;

        // Voice Volume
        voiceVSlider.value = PlayerPrefs.GetFloat("voiceVolume", 0.5f);
        firstVoiceVolume = voiceVSlider.value;

        // Initially disable Apply Button
        ApplyBtn.interactable = false;
    }

    #region Master Volume
    public void SetMasterVolume(float volume)
    {
        CheckApplyState();

        masterVGage.text = $"{volume * 100:0}%";
        float dbVolume = (volume <= 0.0001f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("master", dbVolume);

    }
    #endregion

    #region Background Volume
    public void SetBackgroundVolume(float volume)
    {
        CheckApplyState();

        bgVGage.text = $"{volume * 100:0}%";
        float dbVolume = (volume <= 0.0001f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("background", dbVolume);
    }
    #endregion

    #region Effect Volume
    public void SetEffectVolume(float volume)
    {
        CheckApplyState();

        effectVGage.text = $"{volume * 100:0}%";
        float dbVolume = (volume <= 0.0001f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("effect", dbVolume);
    }
    #endregion

    #region Voice Volume
    public void SetVoiceVolume(float volume)
    {
        CheckApplyState();

        voiceVGage.text = $"{volume * 100:0}%";
        float dbVolume = (volume <= 0.0001f) ? -80f : Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("voice", dbVolume);
    }
    #endregion

    //Apply
    private void ApplySoundSetting()
    {
        //Master Volume
        if (masterVSlider.value != firstMasterVolume)
        {
            PlayerPrefs.SetFloat("masterVolume", masterVSlider.value);
        }

        //Background Volume
        if (bgVSlider.value != firstBgVolume)
        {
            PlayerPrefs.SetFloat("backgroundVolume", bgVSlider.value);
        }

        //Effect Volume
        if (effectVSlider.value != firstEffectVolume)
        {
            PlayerPrefs.SetFloat("effectVolume", effectVSlider.value);
        }

        //Voice Volume
        if (voiceVSlider.value != firstVoiceVolume)
        {
            PlayerPrefs.SetFloat("voiceVolume", voiceVSlider.value);
        }

        ApplyBtn.interactable = false;
    }
    private void CheckApplyState()
    {
        bool isChanged = false;

        // 하나라도 다르면 true
        if (masterVSlider.value != firstMasterVolume) isChanged = true;
        else if (bgVSlider.value != firstBgVolume) isChanged = true;
        else if (effectVSlider.value != firstEffectVolume) isChanged = true;
        else if (voiceVSlider.value != firstVoiceVolume) isChanged = true;

        ApplyBtn.interactable = isChanged;
    }

    //Exit
    private void ExitSoundSetting()
    {
        gameObject.SetActive(false);
    }
}
