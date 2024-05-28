using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class NeededItem
{
    [SerializeField] private SpecialItem item;
    [SerializeField] private bool itemRecieved;

    public SpecialItem Item { get => item; }
    public bool ItemRecieved 
    { 
        get => itemRecieved;
        set
        {
            if (!itemRecieved) itemRecieved = value;
        }
    }
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

    public void RecieveItem(SpecialItem item)
    {
        if (neededItems.Any(NeededItem => NeededItem.Item == item))
        {
            neededItems.Find(NeededItem => NeededItem.Item == item).ItemRecieved = true;
        }
    }
}
