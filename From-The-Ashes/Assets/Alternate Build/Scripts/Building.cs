using System;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public BuildingInformation buildingInformation;
    public Production production;

    [SerializeField] private Button buildingButton;
    public static Action<Building> BuildingButtonClicked;

    private int clickUpgradeCostInWood;
    private int clickUpgradeCostInSteel;
    private int clickUpgradeCostInFuel;
    private int clickUpgradeCostInLead;

    private int passiveUpgradeCostInWood;
    private int passiveUpgradeCostInSteel;
    private int passiveUpgradeCostInFuel;
    private int passiveUpgradeCostInLead;

    public int ClickUpgradeCostInWood { get { return clickUpgradeCostInWood; } }
    public int ClickUpgradeCostInSteel { get => clickUpgradeCostInSteel; set => clickUpgradeCostInSteel = value; }
    public int ClickUpgradeCostInFuel { get => clickUpgradeCostInFuel; set => clickUpgradeCostInFuel = value; }
    public int ClickUpgradeCostInLead { get => clickUpgradeCostInLead; set => clickUpgradeCostInLead = value; }
    public int PassiveUpgradeCostInWood { get { return passiveUpgradeCostInWood; } }
    public int PassiveUpgradeCostInSteel { get => passiveUpgradeCostInSteel; set => passiveUpgradeCostInSteel = value; }
    public int PassiveUpgradeCostInFuel { get => passiveUpgradeCostInFuel; set => passiveUpgradeCostInFuel = value; }
    public int PassiveUpgradeCostInLead { get => passiveUpgradeCostInLead; set => passiveUpgradeCostInLead = value; }

    private void Start()
    {
        clickUpgradeCostInWood = buildingInformation.ClickUpgradeCostInWood;
        clickUpgradeCostInSteel = buildingInformation.ClickUpgradeCostInSteel;
        clickUpgradeCostInFuel = buildingInformation.ClickUpgradeCostInFuel;
        clickUpgradeCostInLead = buildingInformation.ClickUpgradeCostInLead;

        passiveUpgradeCostInWood = buildingInformation.PassiveUpgradeCostInWood;
        passiveUpgradeCostInSteel = buildingInformation.PassiveUpgradeCostInSteel;
        passiveUpgradeCostInFuel = buildingInformation.PassiveUpgradeCostInFuel;
        passiveUpgradeCostInLead = buildingInformation.PassiveUpgradeCostInLead;

        production = GetComponent<Production>();

        buildingButton.onClick.AddListener(() => BuildingButtonClicked(this));
    }

    public void UpgradeClick()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(clickUpgradeCostInWood)
            && NewResources.SteelNeeded.Invoke(clickUpgradeCostInSteel)
            && NewResources.FuelNeeded.Invoke(clickUpgradeCostInFuel)
            && NewResources.LeadNeeded.Invoke(clickUpgradeCostInLead);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(clickUpgradeCostInWood);
            NewResources.SteelConsumed.Invoke(clickUpgradeCostInSteel);
            NewResources.FuelConsumed.Invoke(clickUpgradeCostInFuel);
            NewResources.LeadConsumed.Invoke(clickUpgradeCostInLead);

            production.UpgradeClick();

            clickUpgradeCostInWood += buildingInformation.ClickUpgradeCostInWoodIncrease;
            clickUpgradeCostInSteel += buildingInformation.ClickUpgradeCostInSteel;
            clickUpgradeCostInFuel += buildingInformation.ClickUpgradeCostInFuel;
            clickUpgradeCostInLead += buildingInformation.ClickUpgradeCostInLead;
        }
    }

    public void UpgradePassive()
    {
        bool enoughResources = NewResources.WoodNeeded.Invoke(passiveUpgradeCostInWood)
            && NewResources.SteelNeeded.Invoke(passiveUpgradeCostInSteel)
            && NewResources.FuelNeeded.Invoke(passiveUpgradeCostInFuel)
            && NewResources.LeadNeeded.Invoke(passiveUpgradeCostInLead);

        if (enoughResources)
        {
            NewResources.WoodConsumed.Invoke(passiveUpgradeCostInWood);
            NewResources.SteelConsumed.Invoke(passiveUpgradeCostInSteel);
            NewResources.FuelConsumed.Invoke(passiveUpgradeCostInFuel);
            NewResources.LeadConsumed.Invoke(passiveUpgradeCostInLead);

            production.UpgradePassive();

            passiveUpgradeCostInWood += buildingInformation.PassiveUpgradeCostInWoodIncrease;
            passiveUpgradeCostInSteel += buildingInformation.PassiveUpgradeCostInSteel;
            passiveUpgradeCostInFuel += buildingInformation.PassiveUpgradeCostInFuel;
            passiveUpgradeCostInLead += buildingInformation.PassiveUpgradeCostInLead;
        }
    }

    public void Demolish()
    {
        buildingInformation.DecreaseCurrentCost();
        Destroy(gameObject);
    }
}
