using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

// Ќа случай, если будет ограниченное количество рейдов с разными ресурсами. Ќапример:
// ѕерый рейд: в лес, где нужны патроны и еда (напоминаю, что мы можем добавить любые ресурсы)
// ¬торой рейд: в горы, где нужны патроны и верЄвки
// » т. д.
[Serializable]
public class RaidData
{
    [SerializeField] Button raidButton;
    [SerializeField] private string raidName;
    [SerializeField] private string raidDescription;
    [SerializeField] private List<ResourceContainer> cost;

    public string Description { get => raidDescription; }
    public List<ResourceContainer> Cost { get => cost; }
}

public class MilitaryBase : MonoBehaviour
{
    [SerializeField] private List<RaidData> raidDatas;

    [Header("Menu UI")]
    [SerializeField] private Button militaryBaseButton;
    [SerializeField] private GameObject raidMenuObject;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button raidButton;
    [SerializeField] private float menuClosingDelay = 0.1f;

    private void Awake()
    {
        militaryBaseButton.onClick.AddListener(OpenMenu);
        raidButton.onClick.AddListener(Raid);
        UpdateCostText();
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

    private void UpdateCostText()
    {
        string costString = "";

        foreach (RaidData raidData in raidDatas)
        {
            
        }

        costText.text = costString;
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
