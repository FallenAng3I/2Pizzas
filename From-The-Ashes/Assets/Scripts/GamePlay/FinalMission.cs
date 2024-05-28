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

public class FinalMission : MonoBehaviour
{
    [SerializeField] private List<NeededItem> neededItems;

    // Синглтон, в будущем возможно использование более гибкого ScriptableObject
    public static FinalMission Instance;
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
    }

    private void Start()
    {
        RaidsPanel.OnSpecialItemObtained += RecieveItem;
    }

    public void RecieveItem(SpecialItem item)
    {
        if (neededItems.Any(NeededItem => NeededItem.Item == item && NeededItem.ItemRecieved == false))
        {
            NeededItem neededItem = neededItems.Find(NeededItem => NeededItem.Item == item);
            neededItem.ItemRecieved = true;
            neededItem.Image.sprite = item.Icon;
            neededItem.Image.enabled = true;
            CheckIfAllRecieved();
        }
    }

    private void CheckIfAllRecieved()
    {
        bool allRecieved = true;

        foreach (var item in neededItems)
        {
            allRecieved &= item.ItemRecieved;
        }
    }
}
