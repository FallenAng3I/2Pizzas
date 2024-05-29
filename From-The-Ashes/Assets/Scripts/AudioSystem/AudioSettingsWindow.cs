using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsWindow : MonoBehaviour
{
    [SerializeField] private AudioSettingsData audioSettingsData;
    [SerializeField] private AudioMixer audioMixer;

    [Header("UI Elements")]
    [SerializeField] private Toggle masterToggle;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle effectsToggle;
    [SerializeField] private Slider effectsSlider;

    private void Awake()
    {
        masterToggle.onValueChanged.AddListener((bool enabled) => ToggleSound(enabled, "MasterVolume", masterToggle, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume));
        musicToggle.onValueChanged.AddListener((bool enabled) => ToggleSound(enabled, "MusicVolume", musicToggle, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume));
        effectsToggle.onValueChanged.AddListener((bool enabled) => ToggleSound(enabled, "EffectsVolume", effectsToggle, ref audioSettingsData.effectsEnabled, ref audioSettingsData.effectsVolume));

        masterSlider.onValueChanged.AddListener((float volume) => SetVolume("MasterVolume", Mathf.Clamp(volume, 0.001f, float.MaxValue), masterSlider, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume));
        musicSlider.onValueChanged.AddListener((float volume) => SetVolume("MusicVolume", Mathf.Clamp(volume, 0.001f, float.MaxValue), musicSlider, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume));
        effectsSlider.onValueChanged.AddListener((float volume) => SetVolume("EffectsVolume", Mathf.Clamp(volume, 0.001f, float.MaxValue), effectsSlider, ref audioSettingsData.effectsEnabled, ref audioSettingsData.effectsVolume));
    }

    private void Start()
    {
        SetVolume("MasterVolume", audioSettingsData.masterVolume, masterSlider, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume);
        SetVolume("MusicVolume", audioSettingsData.musicVolume, musicSlider, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume);
        SetVolume("EffectsVolume", audioSettingsData.effectsVolume, effectsSlider, ref audioSettingsData.effectsEnabled, ref audioSettingsData.effectsVolume);

        ToggleSound(audioSettingsData.masterEnabled, "MasterVolume", masterToggle, ref audioSettingsData.masterEnabled, ref audioSettingsData.masterVolume);
        ToggleSound(audioSettingsData.musicEnabled, "MasterVolume", musicToggle, ref audioSettingsData.musicEnabled, ref audioSettingsData.musicVolume);
        ToggleSound(audioSettingsData.effectsEnabled, "EffectsVolume", effectsToggle, ref audioSettingsData.effectsEnabled, ref audioSettingsData.effectsVolume);

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
        }

        enabledData = enabled;
        toggle.isOn = enabled;
    }
}
