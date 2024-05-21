using UnityEngine;

[CreateAssetMenu]
public class BuildingInformation : ScriptableObject
{
    [SerializeField] private string buildingName;
    [SerializeField] private string buildingDescription;
    public string BuildingName { get => buildingName; }
    public string BuildingDescription { get => buildingDescription; }

    [Header("Construction Cost")]
    [SerializeField] private int BaseCostInWood;
    [SerializeField] private int CostInWoodIncrease;
    [SerializeField] private int BaseCostInSteel;
    [SerializeField] private int CostInSteelIncrease;
    [SerializeField] private int BaseCostInFuel;
    [SerializeField] private int CostInFuelIncrease;
    [SerializeField] private int BaseCostInLead;
    [SerializeField] private int CostInLeadIncrease;
    // ����� �������� ���� � ������ ��������

    [HideInInspector] public int CurrentCostInWood;
    [HideInInspector] public int CurrentCostInSteel;
    [HideInInspector] public int CurrentCostInFuel;
    [HideInInspector] public int CurrentCostInLead;

    [Header("Click Upgrade Cost")]
    [SerializeField] private int clickUpgradeCostInWood;
    [SerializeField] private int clickUpgradeCostInWoodIncrease;
    [SerializeField] private int clickUpgradeCostInSteel;
    [SerializeField] private int clickUpgradeCostInSteelIncrease;
    [SerializeField] private int clickUpgradeCostInFuel;
    [SerializeField] private int clickUpgradeCostInFuelIncrease;
    [SerializeField] private int clickUpgradeCostInLead;
    [SerializeField] private int clickUpgradeCostInLeadIncrease;
    // ����� �������� ���� � ������ ��������

    public int ClickUpgradeCostInWood { get => clickUpgradeCostInWood; }
    public int ClickUpgradeCostInWoodIncrease { get => clickUpgradeCostInWoodIncrease; }
    public int ClickUpgradeCostInSteel { get => clickUpgradeCostInSteel; }
    public int ClickUpgradeCostInSteelIncrease { get => clickUpgradeCostInSteelIncrease; }
    public int ClickUpgradeCostInFuel { get => clickUpgradeCostInFuel; }
    public int ClickUpgradeCostInFuelIncrease { get => clickUpgradeCostInFuelIncrease; }
    public int ClickUpgradeCostInLead { get => clickUpgradeCostInLead; }
    public int ClickUpgradeCostInLeadIncrease { get => clickUpgradeCostInLeadIncrease; }

    [Header("Passive Upgrade Cost")]
    [SerializeField] private int passiveUpgradeCostInWood;
    [SerializeField] private int passiveUpgradeCostInWoodIncrease;
    [SerializeField] private int passiveUpgradeCostInSteel;
    [SerializeField] private int passiveUpgradeCostInSteelIncrease;
    [SerializeField] private int passiveUpgradeCostInFuel;
    [SerializeField] private int passiveUpgradeCostInFuelIncrease;
    [SerializeField] private int passiveUpgradeCostInLead;
    [SerializeField] private int passiveUpgradeCostInLeadIncrease;
    // ����� �������� ���� � ������ ��������

    public int PassiveUpgradeCostInWood { get => passiveUpgradeCostInWood; }
    public int PassiveUpgradeCostInWoodIncrease { get => passiveUpgradeCostInWoodIncrease; }
    public int PassiveUpgradeCostInSteel { get => passiveUpgradeCostInSteel; }
    public int PassiveUpgradeCostInSteelIncrease { get => passiveUpgradeCostInSteelIncrease; }
    public int PassiveUpgradeCostInFuel { get => passiveUpgradeCostInFuel; }
    public int PassiveUpgradeCostInFuelIncrease { get => passiveUpgradeCostInFuelIncrease; }
    public int PassiveUpgradeCostInLead { get => passiveUpgradeCostInLead; }
    public int PassiveUpgradeCostInLeadIncrease { get => passiveUpgradeCostInLeadIncrease; }

    public void ResetCurrentCost()
    {
        CurrentCostInWood = BaseCostInWood;
        CurrentCostInSteel = BaseCostInSteel;
        CurrentCostInFuel = BaseCostInFuel;
        CurrentCostInLead = BaseCostInLead;
    }

    public void IncreaseCurrentCost()
    {
        CurrentCostInWood += CostInWoodIncrease;
        CurrentCostInSteel += CostInSteelIncrease;
        CurrentCostInFuel += CostInFuelIncrease;
        CurrentCostInLead += CostInLeadIncrease;
    }

    public void DecreaseCurrentCost()
    {
        CurrentCostInWood -= CostInWoodIncrease;
        CurrentCostInSteel -= CostInSteelIncrease;
        CurrentCostInFuel -= CostInFuelIncrease;
        CurrentCostInLead -= CostInLeadIncrease;
    }
}
