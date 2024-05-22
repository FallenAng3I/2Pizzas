using System;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private Button buildingButton;

    [SerializeField] private BuildingInformation buildingInformation;
    [SerializeField] private Production production;

    public BuildingInformation BuildingInformation { get => buildingInformation; set => buildingInformation = value; }
    public Production Production { get => production; set => production = value; }

    private ConstructionSlot constructionSlot;

    public static event Action<Building> OnBuildingSelected;

    private void Start()
    {
        constructionSlot = GetComponentInParent<ConstructionSlot>();

        buildingButton.onClick.AddListener(SelectBuilding);
    }

    private void SelectBuilding()
    {
        constructionSlot.IndicateSelection(true);
        OnBuildingSelected?.Invoke(this);
    }

    public void UpgradeClick()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(BuildingInformation.CurrentClickUpgradeCostInWood)
            && NewResources.SteelNeeded.Invoke(BuildingInformation.CurrentClickUpgradeCostInSteel)
            && NewResources.FuelNeeded.Invoke(BuildingInformation.CurrentClickUpgradeCostInFuel)
            && NewResources.LeadNeeded.Invoke(BuildingInformation.CurrentClickUpgradeCostInLead);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(BuildingInformation.CurrentClickUpgradeCostInWood);
            NewResources.SteelConsumed.Invoke(BuildingInformation.CurrentClickUpgradeCostInSteel);
            NewResources.FuelConsumed.Invoke(BuildingInformation.CurrentClickUpgradeCostInFuel);
            NewResources.LeadConsumed.Invoke(BuildingInformation.CurrentClickUpgradeCostInLead);

            BuildingInformation.UpgradeClickProduction();

            BuildingInformation.IncreaseCurrentClickUpgradeCost();
        }
    }

    public void UpgradePassive()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInWood)
            && NewResources.SteelNeeded.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInSteel)
            && NewResources.FuelNeeded.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInFuel)
            && NewResources.LeadNeeded.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInLead);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInWood);
            NewResources.SteelConsumed.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInSteel);
            NewResources.FuelConsumed.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInFuel);
            NewResources.LeadConsumed.Invoke(BuildingInformation.CurrentPassiveUpgradeCostInLead);

            BuildingInformation.UpgradePassiveProduction();

            BuildingInformation.IncreaseCurrentPassiveUpgradeCost();
        }
    }

    public void Demolish()
    {
        BuildingInformation.DecreaseCurrentConstructionCost();
        Destroy(gameObject);
    }
}
