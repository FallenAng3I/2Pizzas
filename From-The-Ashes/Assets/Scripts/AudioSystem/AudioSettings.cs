using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioSettingsData audioSettingsData;

    [SerializeField] private AudioMixer audioMixer;

    [Header("Volume Switches")] 
    [SerializeField] private Toggle masterToggle;
    [SerializeField] private Toggle musicToggle;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        ToggleSound(audioSettingsData.masterEnabled, "MasterVolume", masterToggle, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume);
        ToggleSound(audioSettingsData.musicEnabled, "MasterVolume", musicToggle, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume);

        SetVolume("MasterVolume", audioSettingsData.masterVolume, masterSlider, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume);
        SetVolume("MusicVolume", audioSettingsData.musicVolume, musicSlider, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume);

        masterToggle.onValueChanged.AddListener((bool enabled) => ToggleSound(enabled, "MasterVolume", masterToggle, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume));
        musicToggle.onValueChanged.AddListener((bool enabled) => ToggleSound(enabled, "MusicVolume", musicToggle, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume));

        masterSlider.onValueChanged.AddListener((float volume) => SetVolume("MasterVolume", volume, masterSlider, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume));
        musicSlider.onValueChanged.AddListener((float volume) => SetVolume("MusicVolume", volume, musicSlider, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume));
    }

    private void SetVolume(string audioGroupVolume, float volume, Slider volumeSlider, ref bool enabledData, ref float volumeData)
    {
        if (enabledData)
        {
            audioMixer.SetFloat(audioGroupVolume, Mathf.Log10(volume) * 20);
        }
        else
        {
            audioMixer.SetFloat(audioGroupVolume, -80);
        }
        volumeSlider.value = volume;
        volumeData = volume;
    }

    private void ToggleSound(bool enabled, string audioGroupVolume, Toggle toggle, ref bool enabledData, ref float volumeData)
    {
        
        if (enabled)
        {
            audioMixer.SetFloat(audioGroupVolume, Mathf.Log10(volumeData) * 20);
            
        }
        else
        {
            audioMixer.SetFloat(audioGroupVolume, -80);
            Debug.Log("1");
        }

        enabledData = enabled;
        toggle.isOn = enabled;
    }
}
