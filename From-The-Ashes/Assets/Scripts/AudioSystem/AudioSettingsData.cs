using UnityEngine;

[CreateAssetMenu]
public class AudioSettingsData : ScriptableObject
{
    public float masterVolume;
    public float musicVolume;
    public float effectsVolume;

    public bool masterEnabled;
    public bool musicEnabled;
    public bool effectsEnabled;
}
