using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioSource effectSource;

    [SerializeField]
    private AudioMixer audioMixer;

    private float currentVolume;
    private float dbVolume;

    [SerializeField]
    private AudioClip bgmClip;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InitAudioMixer();

        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    private void InitAudioMixer()
    {

        // Master
        currentVolume = PlayerPrefs.GetFloat("masterVolume" , 0.5f);
        dbVolume = (currentVolume <= 0.0001f) ? -80f : Mathf.Log10(currentVolume) * 20;
        audioMixer.SetFloat("master", dbVolume);

        // Background
        currentVolume = PlayerPrefs.GetFloat("backgroundVolume",0.5f);
        dbVolume = (currentVolume <= 0.0001f) ? -80f : Mathf.Log10(currentVolume) * 20;
        audioMixer.SetFloat("background", dbVolume);

        // Effect
        currentVolume = PlayerPrefs.GetFloat("effectVolume",0.5f);
        Debug.Log(currentVolume);
        dbVolume = (currentVolume <= 0.0001f) ? -80f : Mathf.Log10(currentVolume) * 20;
        audioMixer.SetFloat("effect", dbVolume);

        // Voice
        currentVolume = PlayerPrefs.GetFloat("voiceVolume",0.5f);
        dbVolume = (currentVolume <= 0.0001f) ? -80f : Mathf.Log10(currentVolume) * 20;
        audioMixer.SetFloat("voice", dbVolume);
    }

    public void PlaySFX(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

}
