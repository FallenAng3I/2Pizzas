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
    [SerializeField] private string description;
    [SerializeField] private List<Cost> resourcesNeeded;

    public string Description { get => description; }
    public List<Cost> ResourcesNeeded { get => resourcesNeeded; }
}

public class MilitaryBase : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private List<Cost> raidsCosts;

    [Header("Menu UI")]
    [SerializeField] private GameObject raidMenuObject;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button raidButton;
    [SerializeField] private float menuClosingDelay = 0.1f;

    private void Awake()
    {
        raidButton.onClick.AddListener(Raid);
        UpdateCostText();
        CloseMenu();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OpenMenu();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(CloseWithMenuWithDelay());
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

        foreach (Cost cost in raidsCosts)
        {
            cost.ResetCost();
            costString += $"{cost.Resource.name}: {cost.Quantity}\r\n";
        }

        costText.text = costString;
    }

    private void Raid()
    {
        bool enoughResources = true;

        foreach (Cost cost in raidsCosts)
        {
            enoughResources = enoughResources && Storage.Instance.GetResourceAmount(cost.Resource) >= cost.Quantity;
        }

        if (enoughResources)
        {
            foreach (Cost cost in raidsCosts)
            {
                Storage.Instance.SubtractResource(cost.Resource, cost.Quantity);
                cost.IncreaseCost();
            }

            CloseMenu();
        }

    }
}
