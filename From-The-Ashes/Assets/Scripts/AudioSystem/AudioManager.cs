using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private List<BuildingData> buildingsData;
    [SerializeField] private AudioClip buildingDemolishedClip;
    [SerializeField] private AudioClip baseConstructedClip;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private List<AudioClip> musicClips;
    private int nextMusicClip = 0;

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.clip = musicClips[nextMusicClip];
            musicSource.Play();

            if (nextMusicClip + 1 < musicClips.Count)
            {
                nextMusicClip++;
            }
            else
            {
                nextMusicClip = 0;
            }
        }
    }

    private void PlayEffectClip(AudioClip constructionClip)
    {
        effectsSource.PlayOneShot(constructionClip);
    }

    private void PlayMilitaryBaseClip()
    {
        PlayEffectClip(baseConstructedClip);
    }

    private void PlayDemolishedClip()
    {
        PlayEffectClip(buildingDemolishedClip);
    }

    private void OnEnable()
    {
        MilitaryBase.OnMilitaryBaseBuilt += PlayMilitaryBaseClip;
        foreach (var buildingData in buildingsData)
        {
            buildingData.OnBuildingConstructedSound += PlayEffectClip;
            buildingData.OnBuildingDemolished += PlayDemolishedClip;
        }
    }

    private void OnDisable()
    {
        MilitaryBase.OnMilitaryBaseBuilt -= PlayMilitaryBaseClip;
        foreach (var buildingData in buildingsData)
        {
            buildingData.OnBuildingConstructedSound -= PlayEffectClip;
            buildingData.OnBuildingDemolished -= PlayDemolishedClip;
        }
    }
}
