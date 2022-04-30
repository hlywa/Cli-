using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class AudioSettingsManager : Singleton<AudioSettingsManager>
{
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _musicSlider;
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] public AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    private void Start()
    {
       // _masterSlider.value = 0.5f;
       // _musicSlider.value = 0.5f;
    }

    public void _playSFX(AudioClip clipToPlay, bool isLooped)
    {
        _sfxAudioSource.loop = isLooped;
        _sfxAudioSource.clip = clipToPlay;
        _sfxAudioSource.Play();
        
    }

    public void _stopSFX()
    {
        _sfxAudioSource.loop = false;
        _sfxAudioSource.Stop();
    }
    
    
    #region Slider methods
    public void SetMasterVol(float sliderVal)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderVal) * 20);
        AudioLevels.MasterVolume = sliderVal;
    }

    public void SetMusicVol(float sliderVal)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderVal) * 20);
        AudioLevels.MusicVolume = sliderVal;
    }

    public void SetSFXVol(float sliderVal)
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderVal) * 20);
        AudioLevels.SFXVolume = sliderVal;
    }
    #endregion
}
