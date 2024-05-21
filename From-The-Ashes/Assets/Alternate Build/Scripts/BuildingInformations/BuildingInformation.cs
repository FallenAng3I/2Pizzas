using UnityEngine;

[CreateAssetMenu]
public class BuildingInformation : ScriptableObject
{
    [SerializeField] private string buildingName;
    [SerializeField] private string buildingDescription;
    public string BuildingName { get => buildingName; }
    public string BuildingDescription { get => buildingDescription; }

    [Header("Construction Cost")]
    [SerializeField] private int baseConstructionCostInWood;
    [SerializeField] private int constructionCostInWoodIncrease;
    private int currentConstructionCostInWood;
    [SerializeField] private int baseConstructionCostInSteel;
    [SerializeField] private int constructionCostInSteelIncrease;
    private int currentConstructionCostInSteel;
    [SerializeField] private int baseConstructionCostInFuel;
    [SerializeField] private int constructionCostInFuelIncrease;
    private int currentConstructionCostInFuel;
    [SerializeField] private int baseConstructionCostInLead;
    [SerializeField] private int constructionCostInLeadIncrease;
    private int currentConstructionCostInLead;
    // ћожно добавить цены в других ресурсах

    public int CurrentConstructionCostInWood { get => currentConstructionCostInWood; }
    public int CurrentConstructionCostInSteel { get => currentConstructionCostInSteel; }
    public int CurrentConstructionCostInFuel { get => currentConstructionCostInFuel; }
    public int CurrentConstructionCostInLead { get => currentConstructionCostInLead; }

    [Header("Click Production")]
    [SerializeField] private int baseClickProductionQuantity;
    [SerializeField] private int clickProductionQuantityIncrease;
    private int currentClickProductionQuantity;

    public int CurrentClickProductionQuantity { get => currentClickProductionQuantity; }

    [Header("Click Upgrade Cost")]
    [SerializeField] private int baseClickUpgradeCostInWood;
    [SerializeField] private int clickUpgradeCostInWoodIncrease;
    private int currentClickUpgradeCostInWood;
    [SerializeField] private int baseClickUpgradeCostInSteel;
    [SerializeField] private int clickUpgradeCostInSteelIncrease;
    private int currentClickUpgradeCostInSteel;
    [SerializeField] private int baseClickUpgradeCostInFuel;
    [SerializeField] private int clickUpgradeCostInFuelIncrease;
    private int currentClickUpgradeCostInFuel;
    [SerializeField] private int baseClickUpgradeCostInLead;
    [SerializeField] private int clickUpgradeCostInLeadIncrease;
    private int currentClickUpgradeCostInLead;
    // ћожно добавить цены в других ресурсах

    public int CurrentClickUpgradeCostInWood { get => currentClickUpgradeCostInWood; }
    public int CurrentClickUpgradeCostInSteel { get => currentClickUpgradeCostInSteel; }
    public int CurrentClickUpgradeCostInFuel { get => currentClickUpgradeCostInFuel; }
    public int CurrentClickUpgradeCostInLead { get => currentClickUpgradeCostInLead; }
    
    [Header("Passive Production")]
    [SerializeField] private int passiveProductionTime;
    [SerializeField] private int basePassiveProductionQuantity;
    [SerializeField] private int passiveProductionQuantityIncrease;
    private int currentPassiveProductionQuantity;
    private bool passiveProductionUpgraded;

    public bool PassiveProductionUpgraded { get => passiveProductionUpgraded; }
    public int PassiveProductionTime { get => passiveProductionTime; }
    public int CurrentPassiveProductionQuantity { get => currentPassiveProductionQuantity; }

    [Header("Passive Upgrade Cost")]
    [SerializeField] private int basePassiveUpgradeCostInWood;
    [SerializeField] private int passiveUpgradeCostInWoodIncrease;
    private int currentPassiveUpgradeCostInWood;
    [SerializeField] private int basePassiveUpgradeCostInSteel;
    [SerializeField] private int passiveUpgradeCostInSteelIncrease;
    private int currentPassiveUpgradeCostInSteel;
    [SerializeField] private int basePassiveUpgradeCostInFuel;
    [SerializeField] private int passiveUpgradeCostInFuelIncrease;
    private int currentPassiveUpgradeCostInFuel;
    [SerializeField] private int basePassiveUpgradeCostInLead;
    [SerializeField] private int passiveUpgradeCostInLeadIncrease;
    private int currentPassiveUpgradeCostInLead;
    // ћожно добавить цены в других ресурсах

    public int CurrentPassiveUpgradeCostInWood { get => currentPassiveUpgradeCostInWood; }
    public int CurrentPassiveUpgradeCostInSteel { get => currentPassiveUpgradeCostInSteel; }
    public int CurrentPassiveUpgradeCostInFuel { get => currentPassiveUpgradeCostInFuel; }
    public int CurrentPassiveUpgradeCostInLead { get => currentPassiveUpgradeCostInLead; }

    public void ResetCurrentValues()
    {
        currentConstructionCostInWood = baseConstructionCostInWood;
        currentConstructionCostInSteel = baseConstructionCostInSteel;
        currentConstructionCostInFuel = baseConstructionCostInFuel;
        currentConstructionCostInLead = baseConstructionCostInLead;

        currentClickProductionQuantity = baseClickProductionQuantity;

        currentClickUpgradeCostInWood = baseClickUpgradeCostInWood;
        currentClickUpgradeCostInSteel = baseClickUpgradeCostInSteel;
        currentClickUpgradeCostInFuel = baseClickUpgradeCostInFuel;
        currentClickUpgradeCostInLead = baseClickUpgradeCostInLead;

        passiveProductionUpgraded = false;
        currentPassiveProductionQuantity = basePassiveProductionQuantity;

        currentPassiveUpgradeCostInWood = basePassiveUpgradeCostInWood;
        currentPassiveUpgradeCostInSteel = basePassiveUpgradeCostInSteel;
        currentPassiveUpgradeCostInFuel = basePassiveUpgradeCostInFuel;
        currentPassiveUpgradeCostInLead = basePassiveUpgradeCostInLead;
    }

    public void IncreaseCurrentConstructionCost()
    {
        currentConstructionCostInWood += constructionCostInWoodIncrease;
        currentConstructionCostInSteel += constructionCostInSteelIncrease;
        currentConstructionCostInFuel += constructionCostInFuelIncrease;
        currentConstructionCostInLead += constructionCostInLeadIncrease;
    }

    public void DecreaseCurrentConstructionCost()
    {
        currentConstructionCostInWood -= constructionCostInWoodIncrease;
        currentConstructionCostInSteel -= constructionCostInSteelIncrease;
        currentConstructionCostInFuel -= constructionCostInFuelIncrease;
        currentConstructionCostInLead -= constructionCostInLeadIncrease;
    }

    public void UpgradeClickProduction()
    {
        currentClickProductionQuantity += clickProductionQuantityIncrease;
    }

    public void IncreaseCurrentClickUpgradeCost()
    {
        currentClickUpgradeCostInWood += clickUpgradeCostInWoodIncrease;
        currentClickUpgradeCostInSteel += clickUpgradeCostInSteelIncrease;
        currentClickUpgradeCostInFuel += clickUpgradeCostInFuelIncrease;
        currentClickUpgradeCostInLead += clickUpgradeCostInLeadIncrease;
    }

    public void UpgradePassiveProduction()
    {
        if (!passiveProductionUpgraded)
        {
            passiveProductionUpgraded = true;
        }
        else
        {
            currentPassiveProductionQuantity += passiveProductionQuantityIncrease; // ћожно использовать другую форму апгрейда, например, удваивать количество продукта
        }
    }

    public void IncreaseCurrentPassiveUpgradeCost()
    {
        currentPassiveUpgradeCostInWood += passiveUpgradeCostInWoodIncrease;
        currentPassiveUpgradeCostInSteel += passiveUpgradeCostInSteelIncrease;
        currentPassiveUpgradeCostInFuel += passiveUpgradeCostInFuelIncrease;
        currentPassiveUpgradeCostInLead += passiveUpgradeCostInLeadIncrease;
    }
}
