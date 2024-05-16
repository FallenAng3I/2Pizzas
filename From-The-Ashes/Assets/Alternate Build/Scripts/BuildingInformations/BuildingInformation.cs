using UnityEngine;

[CreateAssetMenu]
public class BuildingInformation : ScriptableObject
{
    [SerializeField] public string buildingName;
    [SerializeField] public string buildingDescription;

    [SerializeField] private int CostInWood;
    [HideInInspector] public int CurrentCostInWood;
    [SerializeField] private int CostInWoodIncrease;
    [SerializeField] private int CostInSteel;
    [HideInInspector] public int CurrentCostInSteel;
    [SerializeField] private int CostInSteelIncrease;
    [SerializeField] private int CostInFuel;
    [HideInInspector] public int CurrentCostInFuel;
    [SerializeField] private int CostInFuelIncrease;
    [SerializeField] private int CostInLead;
    [HideInInspector] public int CurrentCostInLead;
    [SerializeField] private int CostInLeadIncrease;

    private void Awake()
    {
        CurrentCostInWood = CostInWood;
        CurrentCostInSteel = CostInSteel;
        CurrentCostInFuel = CostInFuel;
        CurrentCostInLead = CostInLead;
    }

    public void IncreaseCost()
    {
        CurrentCostInWood += CostInWoodIncrease;
        CurrentCostInSteel += CostInSteelIncrease;
        CurrentCostInFuel += CostInFuelIncrease;
        CurrentCostInLead += CostInLeadIncrease;
    }
}
