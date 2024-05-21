using System;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public BuildingInformation buildingInformation;
    public Production production;

    [SerializeField] private Button buildingButton;
    public static Action<Building> BuildingButtonClicked;

    private void Start()
    {
        production = GetComponent<Production>();

        buildingButton.onClick.AddListener(() => BuildingButtonClicked(this));
    }

    public void UpgradeClick()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(buildingInformation.CurrentClickUpgradeCostInWood)
            && NewResources.SteelNeeded.Invoke(buildingInformation.CurrentClickUpgradeCostInSteel)
            && NewResources.FuelNeeded.Invoke(buildingInformation.CurrentClickUpgradeCostInFuel)
            && NewResources.LeadNeeded.Invoke(buildingInformation.CurrentClickUpgradeCostInLead);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(buildingInformation.CurrentClickUpgradeCostInWood);
            NewResources.SteelConsumed.Invoke(buildingInformation.CurrentClickUpgradeCostInSteel);
            NewResources.FuelConsumed.Invoke(buildingInformation.CurrentClickUpgradeCostInFuel);
            NewResources.LeadConsumed.Invoke(buildingInformation.CurrentClickUpgradeCostInLead);

            buildingInformation.UpgradeClickProduction();

            buildingInformation.IncreaseCurrentClickUpgradeCost();
        }
    }

    public void UpgradePassive()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(buildingInformation.CurrentPassiveUpgradeCostInWood)
            && NewResources.SteelNeeded.Invoke(buildingInformation.CurrentPassiveUpgradeCostInSteel)
            && NewResources.FuelNeeded.Invoke(buildingInformation.CurrentPassiveUpgradeCostInFuel)
            && NewResources.LeadNeeded.Invoke(buildingInformation.CurrentPassiveUpgradeCostInLead);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(buildingInformation.CurrentPassiveUpgradeCostInWood);
            NewResources.SteelConsumed.Invoke(buildingInformation.CurrentPassiveUpgradeCostInSteel);
            NewResources.FuelConsumed.Invoke(buildingInformation.CurrentPassiveUpgradeCostInFuel);
            NewResources.LeadConsumed.Invoke(buildingInformation.CurrentPassiveUpgradeCostInLead);

            buildingInformation.UpgradePassiveProduction();

            buildingInformation.IncreaseCurrentPassiveUpgradeCost();
        }
    }

    public void Demolish()
    {
        buildingInformation.DecreaseCurrentConstructionCost();
        Destroy(gameObject);
    }
}
