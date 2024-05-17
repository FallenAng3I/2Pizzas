using System.Collections;
using UnityEngine;

public abstract class ProductionBuilding : MonoBehaviour
{
    protected BuildingInformation buildingInformation;

    protected int clickProductionQuantity;

    protected int clickUpgradeCostInWood;
    protected int clickUpgradeCostInSteel;
    protected int clickUpgradeCostInFuel;
    protected int clickUpgradeCostInLead;

    protected bool passiveProductionUpgraded;
    public bool passiveProductionEnabled = true;

    protected int passiveProductionTime;
    protected int passiveProductionQuantity;

    protected int passiveUpgradeCostInWood;
    protected int passiveUpgradeCostInSteel;
    protected int passiveUpgradeCostInFuel;
    protected int passiveUpgradeCostInLead;

    public int ClickUpgradeCostInWood { get { return clickUpgradeCostInWood; } }
    public int PassiveUpgradeCostInWood { get { return passiveUpgradeCostInWood; } }

    protected IEnumerator ProductionCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(passiveProductionTime);
            if (passiveProductionUpgraded && passiveProductionEnabled) Produce(passiveProductionQuantity);
        }
    }

    protected void Start()
    {
        buildingInformation = GetComponent<Building>().buildingInformation;

        clickProductionQuantity = buildingInformation.BaseClickProductionQuantity;

        clickUpgradeCostInWood = buildingInformation.ClickUpgradeCostInWood;
        clickUpgradeCostInSteel = buildingInformation.ClickUpgradeCostInSteel;
        clickUpgradeCostInFuel = buildingInformation.ClickUpgradeCostInFuel;
        clickUpgradeCostInLead = buildingInformation.ClickUpgradeCostInLead;

        passiveProductionTime = buildingInformation.PassiveProductionTime;
        passiveProductionQuantity = buildingInformation.PassiveProductionQuantity;

        passiveUpgradeCostInWood = buildingInformation.PassiveUpgradeCostInWood;
        passiveUpgradeCostInSteel = buildingInformation.PassiveUpgradeCostInSteel;
        passiveUpgradeCostInFuel = buildingInformation.PassiveUpgradeCostInFuel;
        passiveUpgradeCostInLead = buildingInformation.PassiveUpgradeCostInLead;


    StartCoroutine(ProductionCycle());
    }

    protected void SetParametres()
    {

    }

    protected void ProduceOnClick()
    {
        Produce(clickProductionQuantity);
    }

    public abstract void InvokeAction();

    protected abstract void Produce(int quantity);

    protected abstract void OnEnable();

    protected abstract void OnDisable();

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

            clickProductionQuantity += buildingInformation.ClickProductionQuantityIncrease;// ћожно использовать другую форму апгрейда, например, удваивать количество продукта

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

            if (!passiveProductionUpgraded)
            {
                passiveProductionUpgraded = true;
            }
            else
            {
                passiveProductionQuantity += buildingInformation.PassiveProductionQuantityIncrease; // ћожно использовать другую форму апгрейда, например, удваивать количество продукта
            }

            passiveUpgradeCostInWood += buildingInformation.PassiveUpgradeCostInWoodIncrease;
            passiveUpgradeCostInSteel += buildingInformation.PassiveUpgradeCostInSteel;
            passiveUpgradeCostInFuel += buildingInformation.PassiveUpgradeCostInFuel;
            passiveUpgradeCostInLead += buildingInformation.PassiveUpgradeCostInLead;
        }
    }

    public void TogglePassiveProduction()
    {
        passiveProductionEnabled = !passiveProductionEnabled;
    }
}
