using UnityEngine;

[CreateAssetMenu]
public class BuildingInformation : ScriptableObject
{
    [SerializeField] public string buildingName;
    [SerializeField] public string buildingDescription;

    [Header("Construction Cost")]
    [SerializeField] private int BaseCostInWood;
    [SerializeField] private int CostInWoodIncrease;
    [HideInInspector] public int CurrentCostInWood;
    [SerializeField] private int BaseCostInSteel;
    [SerializeField] private int CostInSteelIncrease;
    [HideInInspector] public int CurrentCostInSteel;
    [SerializeField] private int BaseCostInFuel;
    [SerializeField] private int CostInFuelIncrease;
    [HideInInspector] public int CurrentCostInFuel;
    [SerializeField] private int BaseCostInLead;
    [SerializeField] private int CostInLeadIncrease;
    [HideInInspector] public int CurrentCostInLead;
    // ћожно добавить цены в других ресурсах

    public void ResetCurrentCost()
    {
        CurrentCostInWood = BaseCostInWood;
        CurrentCostInSteel = BaseCostInSteel;
        CurrentCostInFuel = BaseCostInFuel;
        CurrentCostInLead = BaseCostInLead;
    }

    public void IncreaseCost()
    {
        CurrentCostInWood += CostInWoodIncrease;
        CurrentCostInSteel += CostInSteelIncrease;
        CurrentCostInFuel += CostInFuelIncrease;
        CurrentCostInLead += CostInLeadIncrease;
    }

    public void DecreaseCost()
    {
        CurrentCostInWood -= CostInWoodIncrease;
        CurrentCostInSteel -= CostInSteelIncrease;
        CurrentCostInFuel -= CostInFuelIncrease;
        CurrentCostInLead -= CostInLeadIncrease;
    }
}
