using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio sources")]
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
}
