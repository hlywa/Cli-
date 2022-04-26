using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class AudioSettingsManager : Singleton<AudioSettingsManager>
{
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    private void Start()
    {
        _masterSlider.value = 0.5f;
        _musicSlider.value = 0.5f;
        _sfxSlider.value = 0.5f;
    }

    public void _playSFX(AudioClip clipToPlay)
    {
        _sfxAudioSource.clip = clipToPlay;
        _sfxAudioSource.Play();
        
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
