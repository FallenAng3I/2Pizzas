using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Economy/BuildingData", fileName = "New BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private GameObject buildingPrefab;
    public GameObject BuildingPrefab { get => buildingPrefab; }

    [Space]
    [SerializeField] private string buildingName;
    [SerializeField] private string buildingDescription;
    [SerializeField] private Sprite buildingIcon;
    [SerializeField] private AudioClip constructionClip;

    public string BuildingName { get => buildingName; }
    public string BuildingDescription { get => buildingDescription; }
    public Sprite BuildingIcon { get => buildingIcon; }
    public AudioClip ConstructionClip { get => constructionClip; }

    [Header("Construction and Upgrades Cost")]
    [SerializeField] private List<Cost> baseConstructionCost;
    [SerializeField] private List<Cost> baseClickUpgradeCost;
    [SerializeField] private List<Cost> basePassiveUpgradeCost;

    private List<ResourceContainer> currentConstructionCost;
    private List<ResourceContainer> currentClickUpgradeCost;
    private List<ResourceContainer> currentPassiveUpgradeCost;
    public List<ResourceContainer> ConstructionCost { get => currentConstructionCost; }
    public List<ResourceContainer> ClickUpgradeCost { get => currentClickUpgradeCost; }
    public List<ResourceContainer> PassiveUpgradeCost { get => currentPassiveUpgradeCost; }

    [Header("Production")]
    [SerializeField] private List<ResourceContainer> productionInput;
    [SerializeField] private List<ResourceContainer> productionOutput;
    [SerializeField] private int baseClickProductionQuantity = 1;
    [SerializeField] private int clickProductionQuantityIncrease;
    [SerializeField] private int currentClickProductionQuantity;
    [SerializeField] private int passiveProductionTime;
    [SerializeField] private bool passiveProductionUpgraded = false;

    public int ClickProductionQuantityIncrease { get => clickProductionQuantityIncrease; }
    public int CurrentClickProductionQuantity { get => currentClickProductionQuantity; }
    public List<ResourceContainer> ProductionInput { get => productionInput; }
    public List<ResourceContainer> ProductionOutput { get => productionOutput; }
    public int PassiveProductionTime { get => passiveProductionTime; }
    public bool PassiveProductionUpgraded { get => passiveProductionUpgraded; }

    public event Action OnBuildingProduced;
    public void BuildingProduced()
    {
        OnBuildingProduced?.Invoke();
    }

    // Это событие сообщает соответствующему скрипту Production, что по зданию кликнули
    public event Action OnBuildingClicked;
    public void BuildingClicked()
    {
        OnBuildingClicked?.Invoke();
    }

    // Это событие сообщает Production о строительстве нового здания, чтобы увеличить выход пассивного производства
    public event Action OnBuildingConstructed;
    public event Action<AudioClip> OnBuildingConstructedSound;
    public void BuildingConstructed()
    {
        OnBuildingConstructed?.Invoke();
        OnBuildingConstructedSound?.Invoke(constructionClip);
        IncreaseCurrentCost(ref currentConstructionCost, baseConstructionCost);
    }

    // А это событие сообщает Production, что здание было снесено
    public event Action OnBuildingDemolished;
    public void BuildingDemolished()
    {
        OnBuildingDemolished?.Invoke();
        DecreaseCurrentCost(ref currentConstructionCost, baseConstructionCost);
    }

    public void UpgradeClickProduction()
    {
        currentClickProductionQuantity += clickProductionQuantityIncrease;
        IncreaseCurrentCost(ref currentClickUpgradeCost, baseClickUpgradeCost);
    }

    public void UpgradePassiveProduction()
    {
        passiveProductionUpgraded = true;
    }

    public void IncreaseCurrentCost(ref List<ResourceContainer> currentCost, List<Cost> baseCost)
    {
        List<ResourceContainer> newCost = new List<ResourceContainer>();
        for (int i = 0; i < currentCost.Count; i++)
        {
            ResourceContainer resourceContainer = new ResourceContainer(currentCost[i].Resource, currentCost[i].Quantity + baseCost[i].CostIncrease);
            newCost.Add(resourceContainer);
        }
        currentCost = newCost;
    }

    public void DecreaseCurrentCost(ref List<ResourceContainer> currentCost, List<Cost> baseCost)
    {
        List<ResourceContainer> newCost = new List<ResourceContainer>();
        for (int i = 0; i < currentCost.Count; i++)
        {
            ResourceContainer resourceContainer = new ResourceContainer(currentCost[i].Resource, currentCost[i].Quantity - baseCost[i].CostIncrease);
            newCost.Add(resourceContainer);
        }
        currentCost = newCost;
    }

    public void ResetCurrentCost(ref List<ResourceContainer> currentCost, List<Cost> baseCost)
    {
        List<ResourceContainer> newCost = new List<ResourceContainer>();
        foreach (Cost cost in baseCost)
        {
            ResourceContainer resourceContainer = new ResourceContainer(cost.BaseCost.Resource, cost.BaseCost.Quantity);
            newCost.Add(resourceContainer);
        }
        currentCost = newCost;
    }

    // ScriptabeObject сохраняет изменения даже при выходе из PlayMode, поэтому его приходится перезагружать
    // В будущей игре такой проблемы не будет: ScriptableObject не сохраняет изменения между запусками приложения
    public void ResetAllValues()
    {
        currentClickProductionQuantity = baseClickProductionQuantity;
        passiveProductionUpgraded = false;

        ResetCurrentCost(ref currentConstructionCost, baseConstructionCost);
        ResetCurrentCost(ref currentClickUpgradeCost, baseClickUpgradeCost);
        ResetCurrentCost(ref currentPassiveUpgradeCost, basePassiveUpgradeCost);
    }
}
