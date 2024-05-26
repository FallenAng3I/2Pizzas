using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingInformation")]
public class BuildingInformation : ScriptableObject
{
    [SerializeField] private BuildingInformationsList buildingInformationsList;

    [SerializeField] private GameObject buildingPrefab;
    public GameObject BuildingPrefab { get => buildingPrefab; }

    [SerializeField] private string buildingDescription;
    public string BuildingDescription { get => buildingDescription; }

    [Header("Construction and Upgrades Cost")]
    [SerializeField] private List<Cost> constructionCost;
    [SerializeField] private List<Cost> clickUpgradeCost;
    [SerializeField] private List<Cost> passiveUpgradeCost;

    public List<Cost> ConstructionCost { get => constructionCost; }
    public List<Cost> ClickUpgradeCost { get => clickUpgradeCost; }
    public List<Cost> PassiveUpgradeCost { get => passiveUpgradeCost; }

    [Header("Production")]
    [SerializeField] private List<ResourceContainer> productionInput;
    [SerializeField] private List<ResourceContainer> productionOutput;
    [SerializeField] private int baseClickProductionQuantity = 1;
    [SerializeField] private int clickProductionQuantityIncrease;
    [SerializeField] private int currentClickProductionQuantity;
    [SerializeField] private int passiveProductionTime;
    [SerializeField] private bool passiveProductionUpgraded = false;

    public int CurrentClickProductionQuantity { get => currentClickProductionQuantity; }
    public List<ResourceContainer> ProductionInput { get => productionInput; }
    public List<ResourceContainer> ProductionOutput { get => productionOutput; }
    public int PassiveProductionTime { get => passiveProductionTime; }
    public bool PassiveProductionUpgraded { get => passiveProductionUpgraded; }

    private void Awake()
    {
        buildingInformationsList.AddNewBuildingInformation(this);
    }

    // Это событие сообщает соответствующему скрипту Production, что по зданию кликнули
    public event Action OnBuildingClicked;
    public void BuildingClicked()
    {
        OnBuildingClicked?.Invoke();
    }

    // Это событие сообщает Production о строительстве нового здания, чтобы увеличить выход пассивного производства
    public event Action OnBuildingConstructed;
    public void BuildingConstructed()
    {
        OnBuildingConstructed?.Invoke();
    }

    // А это событие сообщает Production, что здание было снесено
    public event Action OnBuildingDemolished;
    public void BuildingDemolished()
    {
        OnBuildingDemolished?.Invoke();
    }

    public void UpgradeClickProduction()
    {
        currentClickProductionQuantity += clickProductionQuantityIncrease;
        foreach (Cost cost in ClickUpgradeCost)
        {
            cost.IncreaseCost();
        }
    }

    public void UpgradePassiveProduction()
    {
        passiveProductionUpgraded = true;
    }

    public void IncreaseCurrentConstructionCost()
    {
        foreach (Cost cost in constructionCost)
        {
            cost.IncreaseCost();
        }
    }

    public void DecreaseCurrentConstructionCost()
    {
        foreach (Cost cost in constructionCost)
        {
            cost.DecreaseCost();
        }
    }

    // ScriptabeObject сохраняет изменения даже при выходе из PlayMode, поэтому его приходится перезагружать
    // В будущей игре такой проблемы не будет: ScriptableObject не сохраняет изменения между запусками приложения
    public void ResetAllValues()
    {
        currentClickProductionQuantity = baseClickProductionQuantity;
        passiveProductionUpgraded = false;

        foreach (Cost cost in constructionCost)
        {
            cost.ResetCost();
        }
        foreach (Cost cost in clickUpgradeCost)
        {
            cost.ResetCost();
        }
        foreach (Cost cost in passiveUpgradeCost)
        {
            cost.ResetCost();
        }
    }
}
