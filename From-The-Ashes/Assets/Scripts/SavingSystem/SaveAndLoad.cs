using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] private AudioSettingsData audioSettings;

    private void Awake()
    {
        LoadGame();
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }
    */
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        Debug.Log("Saving game...");

        string audioSettingData = JsonUtility.ToJson(audioSettings);
        SaveToJson(audioSettingData, "/AudioSettingsData.json");

        Debug.Log("Game saved");
    }

    private void LoadGame()
    {
        Debug.Log("Loading game...");

        string data = LoadFromJson("/AudioSettingsData.json");
        JsonUtility.FromJsonOverwrite(data, audioSettings);

        Debug.Log("Game Loaded");
    }

    private void SaveToJson(string data, string fileName)
    {
        string filePath = Application.persistentDataPath + fileName;
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, data);
        Debug.Log(fileName + " saved");
    }

    private string LoadFromJson(string fileName)
    {
        string filePath = Application.persistentDataPath + fileName;
        string data = System.IO.File.ReadAllText(filePath);
        return data;
    }
}
