using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class NeededItem
{
    [SerializeField] private SpecialItem item;
    [SerializeField] private bool itemRecieved;
    [SerializeField] private Image image;

    public SpecialItem Item { get => item; }
    public bool ItemRecieved 
    { 
        get => itemRecieved;
        set
        {
            if (!itemRecieved) itemRecieved = value;
        }
    }
    public Image Image { get => image; }
}

public class MainMission : MonoBehaviour
{
    [SerializeField] private List<NeededItem> neededItems;

    public static Action OnMissionCompleted;
    public static Action OnMissionFailed;

    // Синглтон, в будущем возможно использование более гибкого ScriptableObject
    public static MainMission Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        RaidsMenu.OnSpecialItemObtained += RecieveItem;
        GameEndTimer.OnTimeEnded += FailMission;
    }

    private void RecieveItem(SpecialItem item)
    {
        if (neededItems.Any(NeededItem => NeededItem.Item == item && NeededItem.ItemRecieved == false))
        {
            NeededItem neededItem = neededItems.Find(NeededItem => NeededItem.Item == item);
            neededItem.ItemRecieved = true;
            neededItem.Image.sprite = item.Icon;
            neededItem.Image.enabled = true;
            CheckIfAllRecieved();
        }
        else
        {
            Debug.Log("Don't need such item");
        }
    }

    private void CheckIfAllRecieved()
    {
        bool allRecieved = true;

        foreach (var item in neededItems)
        {
            allRecieved &= item.ItemRecieved;
        }

        if (allRecieved)
        {
            CompleteMission();
        }
    }

    private void CompleteMission()
    {
        OnMissionCompleted?.Invoke();
    }

    private void FailMission()
    {
        OnMissionFailed?.Invoke();
    }
}
