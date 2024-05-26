using System;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class AudioSettingsData : ScriptableObject
{
    public float masterVolume;
    public float musicVolume;

    public bool masterEnabled;
    public bool musicEnabled;
}
