using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

// Ќа случай, если будет ограниченное количество рейдов с разными ресурсами. Ќапример:
// ѕерый рейд: в лес, где нужны патроны и еда (напоминаю, что мы можем добавить любые ресурсы)
// ¬торой рейд: в горы, где нужны патроны и верЄвки
// » т. д.
[Serializable]
public class RaidData
{
    [SerializeField] private Button raidButton;
    [SerializeField] private string raidName;
    [SerializeField] private string raidDescription;
    [SerializeField] private Sprite raidSprite;

    [SerializeField] private List<ResourceContainer> cost;

    public string Description { get => raidDescription; }
    public List<ResourceContainer> Cost { get => cost; }
    public Button RaidButton { get => raidButton; }
}

public class MilitaryBase : MonoBehaviour
{
    [SerializeField] private List<RaidData> raidDatas;

    [Header("Menu UI")]
    [SerializeField] private Button militaryBaseButton;
    [Space]
    [SerializeField] private GameObject raidMenuObject;
    [SerializeField] private TextMeshProUGUI raidNameText;
    [SerializeField] private TextMeshProUGUI raidCostText;
    [SerializeField] private Button startRaidButton;
    [SerializeField] private float menuClosingDelay = 0.1f;

    private void Awake()
    {
        militaryBaseButton.onClick.AddListener(OpenMenu);

        foreach (RaidData raidData in raidDatas)
        {
            startRaidButton.onClick.AddListener();
        }

        startRaidButton.onClick.AddListener(Raid);

        CloseMenu();
    }

    private void OpenMenu()
    {
        raidMenuObject.SetActive(true);
    }

    private IEnumerator CloseWithMenuWithDelay()
    {
        yield return new WaitForSeconds(menuClosingDelay);
        CloseMenu();
    }

    private void CloseMenu()
    {
        raidMenuObject.SetActive(false);
    }

    private void UpdateMenu()
    {

    }

    private void UpdateCostText()
    {
        string costString = "";

        foreach (RaidData raidData in raidDatas)
        {
            
        }

        raidCostText.text = costString;
    }

    private void Raid()
    {
        bool enoughResources = true;

        foreach (Cost cost in raidDatas)
        {
            enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
        }

        if (enoughResources)
        {
            foreach (Cost cost in raidDatas)
            {
                Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                cost.IncreaseCost();
            }

            CloseMenu();
        }

    }
}
