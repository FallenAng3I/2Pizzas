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

    private void Awake()
    {
        MilitaryBase.OnMilitaryBaseBuilt += () => PlayEffectClip(baseConstructedClip);
        foreach (var buildingData in buildingsData)
        {
            buildingData.OnBuildingConstructed += () => PlayEffectClip(buildingData.ConstructionClip);
            buildingData.OnBuildingDemolished += () => PlayEffectClip(buildingDemolishedClip);
        }
    }

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
}
